using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool notRespawnable;
    public float maxHealth;
    float currentHealth;
    public float experience;
    [HideInInspector] public int number;
    Animator animator;
    bool hasDied = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0 && !hasDied)
        {
            hasDied = true;
            Die(); 
        }
    }

    void Die()
    {
        //animator.Play("death");

        if(Player.instance.currentSpell + maxHealth > Player.instance.startingSpell + (Player.instance.level * 3))
        {
            Player.instance.currentSpell = Player.instance.startingSpell + (Player.instance.level * 3);
        }
        else
        {
            Player.instance.currentSpell += maxHealth;
        }

        Player.instance.experience += experience;
        RoomManager.instance.rooms[RoomManager.instance.currentRoomNumber].aliveEnemies[number] = 0;

        if (notRespawnable)
        {
            RoomManager.instance.rooms[RoomManager.instance.currentRoomNumber].enemies[number] = 0;
        }
    }
}
