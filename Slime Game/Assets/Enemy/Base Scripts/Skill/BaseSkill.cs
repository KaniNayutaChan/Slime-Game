using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkill : MonoBehaviour
{
    public bool stayOnOwner;
    public float damage;
    public float timeTillDestroy;
    [HideInInspector] public GameObject owner;

    public Direction direction;
    public enum Direction
    {
        Default,
        FacePlayer,
        FaceAwayFromOwner
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        if (timeTillDestroy > 0)
        {
            Destroy(gameObject, timeTillDestroy);
        }


        if (direction == Direction.FacePlayer)
        {
            transform.right = Player.instance.transform.position - transform.position;
        }
        else if (direction == Direction.FaceAwayFromOwner)
        {
            transform.right = transform.position - owner.transform.position;
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (stayOnOwner)
        {
            transform.position = owner.transform.position;
        }
    }
}
