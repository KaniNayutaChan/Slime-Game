using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    [HideInInspector] public Rigidbody2D playerRB;

    [Space]
    public float movementSpeed;
    float moveInput;
    [HideInInspector] public bool canMove;

    [Space]
    public float jumpForce;
    public float startJumpTime;
    public Transform feetPos;
    public Transform headPos;
    float jumpTime;
    bool isJumping;
    [HideInInspector] public bool isGrounded;
    public LayerMask whatIsGround;
    public float feetSize;

    [Space]
    public int maxAttacks;
    int attackCounter;
    Vector2 attackVector = new Vector2();
    public float attackForce;
    bool isAttacking;
    bool isHoldingAttack;
    public float startHoldAttackTime;
    float holdAttackTime;

    [Space]
    public int maxLevel;
    public int level = 0;
    public float experience;

    public float startingDamage;
    public float currentDamage;
    public float startingHealth;
    public float currentHealth;
    public float startingSpell;
    public float currentSpell;
    Vector3 sizeVector = new Vector3();

    [Space]
    public float spellDamage;
    public float spellCost;

    [Space]
    public float startHealTime;
    float healTime;
    bool isHealing = false;

    [Space]
    bool hasIFrames;
    public float startIFrameTime;
    float IFrameTime;

    [Space]
    Vector3 shrinkVector = new Vector3();
    public float shrinkSpeed;
    public float minSize;
    public float startShrinkTime;
    float shrinkTime;
    public float startShrinkCooldown;
    float shrinkCooldown;
    bool isShrinking;
    bool isExpanding;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        playerRB = GetComponent<Rigidbody2D>();
        currentDamage = startingDamage + (level * 3);

        HealToFull();

        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForMove();
        CheckForJump();
        CheckForAttack();
        CheckForIFrames();
        CheckForDie();
        CheckForLevelUp();
        CheckForHeal();
        CheckForMinimise();
    }

    private void FixedUpdate()
    {
        if (!isAttacking && canMove && !isHealing)
        {
            transform.position += new Vector3(moveInput, 0, 0) * movementSpeed * Time.deltaTime;
        }
    }

    void CheckForMove()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveInput = -1;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveInput = 1;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            moveInput = 0;
        }
    }

    void CheckForJump()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, feetSize, whatIsGround);

        if (Physics2D.OverlapCircle(headPos.position, feetSize, whatIsGround))
        {
            isJumping = false;
        }

        if(isGrounded)
        {
            attackCounter = 0;
            isAttacking = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpTime = startJumpTime;
            isJumping = true;
            playerRB.velocity = Vector3.up * jumpForce;
        }

        if (isJumping && jumpTime > 0)
        {
            playerRB.velocity = Vector3.up * jumpForce;
            jumpTime -= Time.deltaTime;
        }
        else
        {
            isJumping = false;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }

    void CheckForAttack()
    {
        if(Input.GetKeyDown(KeyCode.X) && attackCounter < maxAttacks && !isGrounded)
        {
            Time.timeScale = 0.5f;
            attackCounter++;
            isJumping = false;
            isAttacking = true;
            isHoldingAttack = true;
            holdAttackTime = startHoldAttackTime;

            if(Input.GetKey(KeyCode.UpArrow))
            {
                attackVector.y = 1;
            }
            else if(Input.GetKey(KeyCode.DownArrow))
            {
                attackVector.y = -1;
            }
            else
            {
                attackVector.y = 0;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                attackVector.x = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                attackVector.x = 1;
            }
            else
            {
                attackVector.x = 0;
            }

            if(!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
            {
                if(IsFacingLeft())
                {
                    attackVector.x = -1;
                }
                else
                {
                    attackVector.x = 1;
                }
            }
        }

        if (Input.GetKey(KeyCode.X) && isHoldingAttack)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                attackVector.y = 1;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                attackVector.y = -1;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                attackVector.x = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                attackVector.x = 1;
            }

            if (holdAttackTime > 0)
            {
                holdAttackTime -= Time.deltaTime;
            }
            else
            {
                isHoldingAttack = false;
                playerRB.velocity = Vector2.zero;
                Time.timeScale = 1;
                playerRB.AddForce(attackVector.normalized * attackForce);
            }

            if(isGrounded)
            {
                isHoldingAttack = false;
                Time.timeScale = 1;
            }
        }


        if (Input.GetKeyUp(KeyCode.X) && isHoldingAttack)
        {
            isHoldingAttack = false;
            playerRB.velocity = Vector2.zero;
            Time.timeScale = 1;
            attackVector.y += 0.3f;
            playerRB.AddForce(attackVector.normalized * attackForce);
        }
    }

    bool IsFacingLeft()
    {
        if (transform.rotation.eulerAngles.y == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void CheckForIFrames()
    {
        if(IFrameTime > 0)
        {
            IFrameTime -= Time.deltaTime;
        }
        else
        {
            hasIFrames = false;
        }
    }

    void CheckForDie()
    {
        if(currentHealth <= 0)
        {
            RoomManager.instance.SpawnRoom(RoomManager.instance.lastSavedRoomNumber, RoomManager.instance.respawnPos);
            Instantiate(RoomManager.instance.transition);
            isJumping = false;
            playerRB.velocity = Vector2.zero;
            canMove = false;
            experience /= 2;
            HealToFull();
        }
    }

    void CheckForLevelUp()
    {
        if(experience >= Mathf.Pow(2, level))
        {
            LevelUp(Mathf.Pow(2, level) - experience);
        }
    }

    void LevelUp(float experienceOverflow)
    {
        level += 1;
        experience = experienceOverflow;
        currentDamage = startingDamage + (level * 3);
        HealToFull();
    }

    void CheckForHeal()
    {
        if(Input.GetKeyDown(KeyCode.Z) && isGrounded)
        {
            healTime = startHealTime;
            isHealing = true;

            if(currentHealth + (currentSpell / 2) > startingHealth + (level * 3))
            {
                currentSpell -= 2 * (startingHealth + (level * 3) - currentHealth);
                currentHealth = startingHealth + (level * 3);
            }
            else
            {
                currentHealth += currentSpell / 2;
                currentSpell = 0;
            }
        }

        if(healTime > 0)
        {
            healTime -= Time.deltaTime;
        }
        else
        {
            isHealing = false;
        }
    }

    void CheckForMinimise()
    {
        if(PowerUpManager.instance.hasMinimise && shrinkCooldown <= 0)
        {
            if(Input.GetKeyDown(KeyCode.LeftControl))
            {
                shrinkCooldown = startShrinkCooldown;
                isShrinking = true;
                shrinkTime = startShrinkTime;
                shrinkVector.Set((transform.localScale.x - minSize), (transform.localScale.x - minSize), (transform.localScale.x - minSize));
            }
        }

        if (isShrinking)
        {
            if (transform.localScale.x > minSize)
            {
                transform.localScale -= shrinkVector * shrinkSpeed * Time.deltaTime;
            }
            else if (shrinkTime > 0)
            {
                shrinkTime -= Time.deltaTime;
            }
            else
            {
                isShrinking = false;
                isExpanding = true;
            }
        }
        if(isExpanding)
        {
            if (transform.localScale.x < 0.3f + (0.02f * currentHealth))
            {
                transform.localScale += shrinkVector * shrinkSpeed * Time.deltaTime;
            }
            else
            {
                isExpanding = false;
            }
        }

        if(shrinkCooldown > 0)
        {
            shrinkCooldown -= Time.deltaTime;
        }
    }

    public void Save()
    {
        HealToFull();
        RoomManager.instance.RespawnEnemies();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Door"))
        {
            RoomManager.instance.SpawnRoom(collision.GetComponent<Door>().connectedRoom, collision.GetComponent<Door>().spawnPos);
            Instantiate(RoomManager.instance.transition);
            isJumping = false;
            playerRB.velocity = Vector2.zero;
            canMove = false;

            Destroy(collision);
        }

        if (collision.CompareTag("Skill"))
        {
            if (!hasIFrames)
            {
                currentHealth -= 1;
                SetSizeToHealth();
                hasIFrames = true;
                IFrameTime = startIFrameTime;
            }
        }
    }

    void HealToFull()
    {
        currentSpell = startingSpell + (level * 3);
        currentHealth = startingHealth + (level * 3);
        SetSizeToHealth();
    }

    void SetSizeToHealth()
    {
        if (isShrinking || isExpanding)
        {
            sizeVector.Set(0.3f + (0.02f * currentHealth), 0.3f + (0.02f * currentHealth), 0.3f + (0.02f * currentHealth));
            transform.localScale = sizeVector;
        }
    }
}
