using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float damage;
    public float timeTillDestroy;
    List<GameObject> damagedEnemies = new List<GameObject>();
    
    public Type type;
    public enum Type
    {
        Attack,
        Spell,
        Barrier
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeTillDestroy);

        transform.localScale = new Vector3(Player.instance.transform.localScale.x, Player.instance.transform.localScale.x, Player.instance.transform.localScale.x);
    }

    // Update is called once per frame
    void Update()
    {
        if(type == Type.Attack || type == Type.Barrier)
        {
            transform.position = Player.instance.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Arena"))
        {
            if (type == Type.Spell)
            {
                Destroy(gameObject);
            }
        }

        if (collision.CompareTag("Enemy"))
        {
            if (type == Type.Attack)
            {
                Player.instance.currentSoul += damage / 3;
            }

            bool hasDamaged = false;
            for (int i = 0; i < damagedEnemies.Count; i++)
            {
                if(damagedEnemies[i] == collision.gameObject)
                {
                    hasDamaged = true;
                }
            }

            if(!hasDamaged)
            {
                damagedEnemies.Add(collision.gameObject);
                collision.GetComponent<BaseEnemyHealth>().TakeDamage(damage, type);

            }
        }
    }
}
