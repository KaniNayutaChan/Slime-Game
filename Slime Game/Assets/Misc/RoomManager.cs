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

    public bool[] defeatedBosses = new bool[8];

    public EnemyList[] enemyList;
    [System.Serializable]
    public class EnemyList
    {
        public GameObject[] skinList;
    }

    [System.Serializable]
    public class Enemy
    {
        public int enemyNumber;
        public Vector3 position;
        [HideInInspector] public int skinNumber;
    }

    public RoomList[] rooms;
    [System.Serializable]
    public class RoomList
    {
        [Space]
        public GameObject room;

        [Space]
        public Enemy[] enemies;
        [HideInInspector] public Enemy[] aliveEnemies;
    }

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
            for (int j = 0; j < rooms[i].enemies.Length; j++)
            {
                rooms[i].enemies[j].skinNumber = Random.Range(0, enemyList[rooms[i].enemies[j].enemyNumber].skinList.Length);
            }

            rooms[i].aliveEnemies = (Enemy[])rooms[i].enemies.Clone();
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
            if(rooms[room].aliveEnemies[i].enemyNumber != 0)
            {
                GameObject enemy = Instantiate(enemyList[rooms[room].aliveEnemies[i].enemyNumber].skinList[rooms[room].aliveEnemies[i].skinNumber], rooms[room].aliveEnemies[i].position, transform.rotation, currentRoom.transform);
                enemy.GetComponent<BaseEnemyHealth>().number = i;
            }
        }
    }
}
