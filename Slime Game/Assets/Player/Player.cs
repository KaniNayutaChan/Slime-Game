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

    [Space]
    public float jumpForce;
    public float startJumpTime;
    public Transform feetPos;
    float jumpTime;
    bool isJumping;
    public bool isGrounded;
    public LayerMask whatIsGround;
    public Vector2 feetSize;

    [Space]
    public float attackUpForce;
    public float attackSideForce;
    public float startAttackCooldown;
    float attackCooldown;
    [HideInInspector] public bool hasAttack;
    Vector2 attackVector = new Vector2();

    [Space]
    public float maxHealth;
    float currentHealth;

    [Space]
    bool hasIFrames;
    public float startIFrameTime;
    float IFrameTime;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        playerRB = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAttack)
        {
            Move();
        }

        Jump();
        Attack();
        IFrames();
        Die();
    }

    private void FixedUpdate()
    {
        if (hasAttack)
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
        isGrounded = Physics2D.OverlapBox(feetPos.position, feetSize, whatIsGround);
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

    void Attack()
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Door"))
        {
            Destroy(RoomManager.instance.currentRoom);
            RoomManager.instance.currentRoom = Instantiate(RoomManager.instance.listOfRooms[collision.GetComponent<Door>().connectedRoom]); ;
            transform.position = collision.GetComponent<Door>().spawnPos;
            playerRB.velocity = Vector2.zero;
        }

        if (collision.CompareTag("Skill"))
        {
            if (!hasIFrames)
            {
                currentHealth -= 1;
                hasIFrames = true;
                IFrameTime = startIFrameTime;
            }
        }
    }
}
