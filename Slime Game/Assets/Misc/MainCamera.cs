using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    Vector3 position = new Vector3();
    public float yOffSet;
    public float zOffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        position.Set(Player.instance.transform.position.x, Player.instance.transform.position.y + yOffSet, -zOffset);
        transform.position = position;
    }
}
