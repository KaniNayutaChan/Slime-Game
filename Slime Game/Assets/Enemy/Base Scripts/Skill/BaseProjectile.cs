using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : BaseSkill
{
    public bool destroyOnArena;
    public float speed;
    public float rotationSpeed;
    public Vector3 startRotation;
    public Vector3 forceVector;
    Rigidbody2D rb;

    public Direction direction;
    public enum Direction
    {
        Default,
        FacePlayer,
        FaceAwayFromOwner
    }

    public enum ProjectileType 
    {
        Linear,
        Homing,
        Parabolic
    }
    public ProjectileType projectileType;

    // Start is called before the first frame update
    public override void Start()
    {
        if(direction == Direction.FacePlayer)
        {
            transform.right = Player.instance.transform.position - transform.position;
        }
        else if(direction == Direction.FaceAwayFromOwner)
        {
            transform.right = transform.position - owner.transform.position;
        }
        else
        {
            transform.rotation = Quaternion.Euler(startRotation);
        }

        if(projectileType == ProjectileType.Parabolic)
        {
            rb = GetComponent<Rigidbody2D>();
            float x = Random.Range(-forceVector.x, forceVector.x);
            float y = Random.Range(forceVector.y, forceVector.z);
            Vector2 force = new Vector2(x, y);
            rb.AddForce(force);
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if(projectileType == ProjectileType.Homing)
        {
            Vector3 targetDirection = Player.instance.transform.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.right, targetDirection, rotationSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (destroyOnArena)
        {
            if (collision.CompareTag("Arena"))
            {
                Destroy(gameObject);
            }
        }
    }
}
