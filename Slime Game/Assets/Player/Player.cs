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
    bool isAttacking;

    [Space]
    public int maxLevel;
    public int level = 0;
    public float startingDamage;
    public float startingHealth;
    public float experience;
    public float currentHealth;
    public float currentDamage;
    Vector3 sizeVector = new Vector3();

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
        currentHealth = startingHealth + (level * 3);
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Attack();
        IFrames();
        Die();
        Experience();
    }

    private void FixedUpdate()
    {
        if (hasAttack && canMove)
        {
            transform.position += new Vector3(moveInput, 0, 0) * movementSpeed * Time.deltaTime;
        }
    }

    void Move()
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

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, feetSize, whatIsGround);

        if(isGrounded)
        {
            hasAttack = true;
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

    void Attack()
    {
        if(Input.GetKeyDown(KeyCode.X) && hasAttack && attackCooldown < 0)
        {
            hasAttack = false;
            isJumping = false;
            isAttacking = true;
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

    void IFrames()
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

    void Die()
    {
        if(currentHealth <= 0)
        {

        }
    }

    void Experience()
    {
        if(experience >= Mathf.Pow(3, level))
        {
            LevelUp(Mathf.Pow(3, level) - experience);
        }
    }

    void LevelUp(float experienceOverflow)
    {
        level += 1;
        experience = experienceOverflow;
        currentDamage = startingDamage + (level * 3);
        currentHealth = startingHealth + (level * 3);
        sizeVector.Set(0.5f + (0.01f * currentHealth), 0.5f + (0.01f * currentHealth), 0.5f + (0.01f * currentHealth));
        transform.localScale = sizeVector;
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
                sizeVector.Set(0.5f + (0.05f * currentHealth), 0.5f + (0.05f * currentHealth), 0.5f + (0.05f * currentHealth));
                transform.localScale = sizeVector;
                hasIFrames = true;
                IFrameTime = startIFrameTime;
            }
        }

        if(collision.CompareTag("Enemy"))
        {
            if(isAttacking)
            {
                collision.GetComponent<Enemy>().health -= currentDamage;
            }
        }
    }
}
