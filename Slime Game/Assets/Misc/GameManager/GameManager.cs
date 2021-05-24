using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject pause;
    bool isPaused = false;
    public bool isNewGame;
    float currentTimeScale = 1;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        if(isNewGame)
        {
            Player.instance.level = Player.instance.maxLevel - 1;
            Player.instance.LevelUp(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPaused)
            {
                currentTimeScale = Time.timeScale;
                isPaused = true;
                Time.timeScale = 0;
                pause.SetActive(true);
            }
            else
            {
                isPaused = false;
                Time.timeScale = currentTimeScale;
                pause.SetActive(false);
            }
        }
    }
}
