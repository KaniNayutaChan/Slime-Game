using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool notRespawnable;
    public float health;
    public float experience;
    public int number;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            //animator.Play("death");
            Player.instance.experience += experience;
            Player.instance.currentSpell += health;
            RoomManager.instance.rooms[RoomManager.instance.currentRoomNumber].aliveEnemies[number] = null;

            if(notRespawnable)
            {
                RoomManager.instance.rooms[RoomManager.instance.currentRoomNumber].enemies[number] = null;
            }
        }
    }
}
