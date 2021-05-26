using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkill : MonoBehaviour
{
    public bool stayOnOwner;
    public float damage;
    public float timeTillDestroy;
    [HideInInspector] public GameObject owner;

    // Start is called before the first frame update
    public virtual void Start()
    {
        if (timeTillDestroy > 0)
        {
            Destroy(gameObject, timeTillDestroy);
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        transform.position = owner.transform.position;
    }
}
