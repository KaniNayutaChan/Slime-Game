using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkill : MonoBehaviour
{
    public float damage;
    public float timeTillDestroy;

    // Start is called before the first frame update
    void Start()
    {
        if (timeTillDestroy > 0)
        {
            Destroy(gameObject, timeTillDestroy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
