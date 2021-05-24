using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : BaseSkill
{
    public float speed;
    public float rotationSpeed;
    public Vector3 startRotation;

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
        Homing
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
}
