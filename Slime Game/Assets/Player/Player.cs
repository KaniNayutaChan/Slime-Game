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
    public Transform grounded;
    float jumpTime;
    bool isJumping;
    [HideInInspector] public bool isGrounded;

    [Space]
    public float attackUpForce;
    public float attackSideForce;
    [HideInInspector] public bool hasAttack;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Attack();
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

        transform.position += new Vector3(moveInput, 0, 0) * movementSpeed * Time.deltaTime;
    }

    void Jump()
    {
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
        if(Input.GetKeyDown(KeyCode.X) && hasAttack)
        {
            hasAttack = false;
            playerRB.velocity = Vector2.zero;

            if (IsFacingLeft())
            {
                playerRB.AddForce(new Vector2(-attackSideForce, attackUpForce));
            }
            else
            {
                playerRB.AddForce(new Vector2(attackSideForce, attackUpForce));
            }
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
}
