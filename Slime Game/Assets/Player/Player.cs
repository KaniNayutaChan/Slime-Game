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
    float jumpTime;
    bool isJumping;
    [HideInInspector] public bool isGrounded;
    public LayerMask whatIsGround;
    public float feetSize;

    [Space]
    public float attackUpForce;
    public float attackSideForce;
    public float startAttackCooldown;
    float attackCooldown;
    [HideInInspector] public bool hasAttack;
    Vector2 attackVector = new Vector2();

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
    bool hasIFrames;
    public float startIFrameTime;
    float IFrameTime;

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
    }

    private void FixedUpdate()
    {
        if (hasAttack && canMove)
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

        if(isGrounded)
        {
            hasAttack = true;
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
        if(Input.GetKeyDown(KeyCode.X) && hasAttack && attackCooldown < 0)
        {
            hasAttack = false;
            isJumping = false;
            attackCooldown = startAttackCooldown;
            playerRB.velocity = Vector2.zero;

            if (IsFacingLeft())
            {
                attackVector.Set(-attackSideForce, attackUpForce);
            }
            else
            {
                attackVector.Set(attackSideForce, attackUpForce);
            }

            playerRB.AddForce(attackVector);
        }

        if (attackCooldown >= 0)
        {
            attackCooldown -= Time.deltaTime;
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
        if(Input.GetKeyDown(KeyCode.Z))
        {
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
        sizeVector.Set(0.3f + (0.02f * currentHealth), 0.3f + (0.02f * currentHealth), 0.3f + (0.02f * currentHealth));
        transform.localScale = sizeVector;
    }
}
