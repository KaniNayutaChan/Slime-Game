using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager instance;

    public bool hasSpell = false;
    public bool hasMinimise = false;
    public bool hasStickToWalls = false;
    public bool hasResistance = false;
    public bool hasDoubleAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
