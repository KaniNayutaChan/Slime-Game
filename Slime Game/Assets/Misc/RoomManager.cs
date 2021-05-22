using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;
    public GameObject[] listOfRooms;

    [HideInInspector] public GameObject currentRoom;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
        {
            instance = this;
        }

        if(currentRoom == null)
        {
            currentRoom = Instantiate(listOfRooms[0]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
