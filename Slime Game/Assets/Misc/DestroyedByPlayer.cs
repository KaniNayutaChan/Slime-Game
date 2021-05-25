using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedByPlayer : MonoBehaviour
{
    public PlayerAttack.Type type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Attack"))
        {
            PlayerAttack attack = collision.GetComponent<PlayerAttack>();

            if(attack.type == type)
            {
                Destroy(gameObject);
            }
        }
    }
}
