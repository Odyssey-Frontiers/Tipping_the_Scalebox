using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public PlayerScaleControls[] objectControllers;
    public Transform[] camPositions;
    public Transform mainCamera;
    public GameObject mainMenu;
    public Slider scaleSlider;
    public Slider positionSlider;
    public GameObject basicsTutorial;
    public GameObject moveTutorial;
    public GameObject spinnerTutorial;
    public GameObject scaleObstacleTutorial;
    public TextMeshProUGUI levelNumText;
    public AudioSource startMusic;
    public AudioSource backgroundMusic;

    public int currentLevel = 1;

    public static GameManager instance;

    public bool inLevel;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        inLevel = false;
        RunningManager.instance.LevelStart();
    }

    public void StartLevel(int levelNumber)
    {
        currentLevel = levelNumber;
        mainCamera.position = camPositions[currentLevel - 1].position;
        objectControllers[currentLevel - 1].SetObjects();
        if (currentLevel == 1) basicsTutorial.SetActive(true);
        else if (currentLevel == 2) moveTutorial.SetActive(true);
        else if (currentLevel == 4) scaleObstacleTutorial.SetActive(true);
        else if (currentLevel == 5) spinnerTutorial.SetActive(true);
        levelNumText.text = currentLevel.ToString();
        mainMenu.SetActive(false);
        inLevel = true;
        startMusic.Stop();
        backgroundMusic.Play();
    }

    public void DropObjects()
    {
        objectControllers[currentLevel - 1].DropObjects();
    }

    public void CompleteLevel()
    {
        RunningManager.instance.lastLevelCompleted = currentLevel;
    }

    public void MoveUpLevel()
    {
        currentLevel++;
        mainCamera.position = camPositions[currentLevel - 1].position;
        positionSlider.value = 0;
        scaleSlider.value = scaleSlider.minValue;

        if (currentLevel == 1) basicsTutorial.SetActive(true);
        else if (currentLevel == 2) moveTutorial.SetActive(true);
        else if (currentLevel == 4) scaleObstacleTutorial.SetActive(true);
        else if (currentLevel == 5) spinnerTutorial.SetActive(true);

        levelNumText.text = currentLevel.ToString();
    }

    public void ExitGame()
    {
        #if UNITY_STANDALONE
            Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void Retry()
    {
        RunningManager.instance.restartLevel = currentLevel;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GoHome()
    {
        RunningManager.instance.isGameStart = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
