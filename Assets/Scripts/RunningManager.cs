using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningManager : MonoBehaviour
{
    public bool isGameStart = true;

    public static RunningManager instance;

    public int restartLevel;
    public int lastLevelCompleted;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // In first scene, make us the singleton.
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
            Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.
    }

    public void LevelStart()
    {
        if (!isGameStart)
        {
            GameManager.instance.mainMenu.SetActive(false);
            GameManager.instance.StartLevel(restartLevel);
            GameManager.instance.inLevel = true;
        }
        else if (isGameStart) isGameStart = false;
    }
}
