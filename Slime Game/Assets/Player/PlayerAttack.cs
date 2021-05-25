using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float damage;
    public float timeTillDestroy;

    public enum Type
    {
        Attack,
        Spell,
        Barrier
    }

    public Type type;

    // Start is called before the first frame update
    void Start()
    {
        if(type == Type.Attack)
        {
            Destroy(gameObject, timeTillDestroy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(type == Type.Attack)
        {
            transform.position = Player.instance.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Arena"))
        { 
            Destroy(gameObject);
        }

        if (collision.CompareTag("Enemy"))
        {
            if (type == Type.Attack)
            {
                Player.instance.currentSpell += damage / 3;
            }
        }
    }
}
