using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager instance;

    public bool hasAttack = false;
    public bool hasSpell = false;
    public bool hasMinimise = false;
    public bool hasWallJump = false;
    public bool hasResistance = false;
    public bool hasDoubleAttack = false;
    public bool hasBarrier = false;

    [System.Serializable]
    public class Charms
    {
        public string name = "";
        public string description = "";
        public bool acquired = false;
        public bool equipped = false;
    }

    public Charms[] charmList;
    public int noOfCharmSlots = 1;

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
        noOfCharmSlots = 1 + Mathf.FloorToInt(Player.instance.level / 3);
    }

    public void GrantPowerUp(int bossNumber)
    {
        switch(bossNumber)
        {
            case 0:
                hasAttack = true;
                break;
            case 1:
                hasMinimise = true;
                break;
            case 2:
                hasSpell = true;
                break;
            case 3:
                hasWallJump = true;
                break;
            case 4:
                hasResistance = true;
                break;
            case 5:
                hasDoubleAttack = true;
                break;
            case 6:
                hasBarrier = true;
                break;
        }
    }
}
