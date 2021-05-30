using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectileSkill : BaseSkill
{
    public float timeTillMove;
    public bool destroyOnArena;
    public float speed;
    float currentSpeed;
    public float rotationSpeed;
    public Vector3 forceVector;
    Rigidbody2D rb;

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
        base.Start();

        currentSpeed = 0;

        if (projectileType == ProjectileType.Parabolic)
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
        if(timeTillMove > 0)
        {
            timeTillMove -= Time.deltaTime;
        }
        else
        {
            currentSpeed = speed;
        }

        transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);

        if (projectileType == ProjectileType.Homing)
        {
            Vector3 targetDirection = Player.instance.transform.position - transform.position;
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (destroyOnArena)
        {
            if (collision.CompareTag("Arena"))
            {
                StartCoroutine(DestroyAfterTime());
            }
        }
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
