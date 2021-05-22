using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;

    public GameObject transition;
    public GameObject[] listOfRooms;

    [HideInInspector] public int lastSavedRoomNumber;
    [HideInInspector] public Vector2 respawnPos;

    [HideInInspector] public GameObject currentRoom;
    [HideInInspector] public int currentRoomNumber;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
        {
            instance = this;
        }

        if (currentRoom == null)
        {
            currentRoom = Instantiate(listOfRooms[0]);
        }
    }

    public void SpawnRoom(int room, Vector3 spawnPos)
    {
        StartCoroutine(InstantiateRoom(room, spawnPos));
    }

    IEnumerator InstantiateRoom(int room, Vector3 spawnPos)
    {
        yield return new WaitForSeconds(1.3f);
        Destroy(RoomManager.instance.currentRoom);
        currentRoomNumber = room;
        currentRoom = Instantiate(listOfRooms[room]);
        Player.instance.transform.position = spawnPos;
        Player.instance.canMove = true;
    }
}
