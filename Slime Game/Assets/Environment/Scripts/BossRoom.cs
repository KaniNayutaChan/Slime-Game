using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    public int boss;
    public GameObject bossDoor1;
    public GameObject bossDoor2;
    public float minXTrigger;
    public float maxXTrigger;
    // Start is called before the first frame update
    void Start()
    {
        SetDoorActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!RoomManager.instance.defeatedBosses[boss])
        {
            if (Player.instance.transform.position.x > minXTrigger && Player.instance.transform.position.x < maxXTrigger)
            {
                SetDoorActive(true);
            }
        }
        else
        {
            StartCoroutine(DestroyDoor());
        }
    }

    IEnumerator DestroyDoor()
    {
        yield return new WaitForSeconds(2);

        SetDoorActive(false);
    }

    void SetDoorActive(bool setOpen)
    {
        if (setOpen)
        {
            bossDoor1.SetActive(true);
            bossDoor2.SetActive(true);
        }
        else
        {
            bossDoor1.SetActive(false);
            bossDoor2.SetActive(false);
        }
    }
}
