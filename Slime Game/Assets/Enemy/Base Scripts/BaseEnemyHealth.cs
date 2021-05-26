using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float experience;
    protected Animator animator;
    protected bool hasDied;
    [HideInInspector] public int number;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        hasDied = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(currentHealth <= 0 && !hasDied)
        {
            Die();
            hasDied = true;
        }
    }

    protected virtual void Die()
    {
        //animator.Play("Die");

        if(Player.instance.currentSpell + maxHealth > Player.instance.startingSpell + (Player.instance.level * 3))
        {
            Player.instance.currentSpell = Player.instance.startingSpell + (Player.instance.level * 3);
        }
        else
        {
            Player.instance.currentSpell += maxHealth;
        }

        Player.instance.experience += experience;

        RoomManager.instance.rooms[RoomManager.instance.currentRoomNumber].aliveEnemies[number].enemyNumber = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            PlayerAttack attack = collision.GetComponent<PlayerAttack>();

            if (currentHealth - attack.damage <= 0 && attack.type != PlayerAttack.Type.Attack)
            {
                currentHealth = 1;
            }
            else
            {
                currentHealth -= attack.damage;
            }
        }
    }
}
