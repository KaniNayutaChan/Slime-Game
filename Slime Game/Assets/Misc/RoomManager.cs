using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;

    public int debugSpawnRoomNumber;
    public Vector2 debugSpawnPosition;

    public GameObject transition;

    [HideInInspector] public int lastSavedRoomNumber;
    [HideInInspector] public Vector2 respawnPos;

    [HideInInspector] public GameObject currentRoom;
    [HideInInspector] public int currentRoomNumber;

    public GameObject[] enemyList;

    public bool[] defeatedBosses = new bool[8];

    [System.Serializable]
    public class RoomList
    {
        [Space]
        public GameObject room;

        [Space]
        public int[] enemies;
        public Vector2[] positions;
        
        [HideInInspector] public int[] aliveEnemies;
    }

    public RoomList[] rooms;

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
#if DEBUG
            SpawnRoom(debugSpawnRoomNumber, debugSpawnPosition);

#else
            SpawnRoom(lastSavedRoomNumber, Vector3.zero);
#endif
        }

        for (int i = 0; i < defeatedBosses.Length; i++)
        {
            defeatedBosses[i] = false;
        }

        RespawnEnemies();
    }

    public void RespawnEnemies()
    {
        for (int i = 0; i < rooms.Length; i++)
        {
            rooms[i].aliveEnemies = (int[])rooms[i].enemies.Clone();
        }
    }

    public void SpawnRoom(int room, Vector3 spawnPos)
    {
        StartCoroutine(InstantiateRoom(room, spawnPos));
    }

    IEnumerator InstantiateRoom(int room, Vector3 spawnPos)
    {
        yield return new WaitForSeconds(1.3f);
        Destroy(currentRoom);
        currentRoomNumber = room;
        currentRoom = Instantiate(rooms[room].room);
        Player.instance.transform.position = spawnPos;
        Player.instance.canMove = true;

        for (int i = 0; i < rooms[room].aliveEnemies.Length; i++)
        {
            if(rooms[room].aliveEnemies[i] != 0)
            {
                GameObject enemy = Instantiate(enemyList[rooms[room].aliveEnemies[i]], rooms[room].positions[i], transform.rotation, currentRoom.transform);
                enemy.GetComponent<BaseEnemyHealth>().number = i;
            }
        }
    }
}
