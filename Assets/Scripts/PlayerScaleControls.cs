using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScaleControls : MonoBehaviour
{
    private float objectScale;

    [SerializeField] private int level;

    public Slider scaleSlider;
    public Slider positionSlider;
    public float maxMoveDist;
    public GameObject invisibleFloor;
    public GameObject enemySideShade;

    public Transform[] playerObjects;
    public Transform[] scalableObstacles;
    private Vector3[] originalPositions;
    private Vector3[] originalScales;
    private Vector3[] originalObstacleScales;

    private bool droppingObjects;

    // Start is called before the first frame update
    void Start()
    {
        // Save the original positions of the objects
        SaveOriginalStats();

        //assign scale slider
        scaleSlider.onValueChanged.AddListener(delegate { ScaleValueChanged(scaleSlider.value); });

        //have slider scale obstacles too
        scaleSlider.onValueChanged.AddListener(delegate { ScaleObstacles(scaleSlider.value); });

        //assign position slider
        positionSlider.onValueChanged.AddListener(delegate { MoveObjects(positionSlider.value); });

        //set all objects to not move by gravity
        foreach (Transform playerObject in playerObjects)
        {
            playerObject.GetComponent<Rigidbody>().useGravity = false;
            playerObject.GetComponent<Rigidbody>().drag = 100;
        }
    }

    public void SetObjects()
    {
        if (originalScales != null)
            ScaleValueChanged(scaleSlider.value);
        if (originalPositions != null)
            MoveObjects(positionSlider.value);
        if (originalObstacleScales != null)
            ScaleObstacles(scaleSlider.value);
    }

    private void ScaleValueChanged(float value)
    {
        if (level != GameManager.instance.currentLevel) return;
        if (droppingObjects) return;

        /*foreach (Transform playerObject in playerObjects)
        {
            playerObject.GetComponent<Rigidbody>().mass = value;
            playerObject.localScale = new Vector3(value, value, value);
        }*/

        for (int i = 0; i < playerObjects.Length; i++)
        {
            playerObjects[i].GetComponent<Rigidbody>().mass = 10*value;
            playerObjects[i].localScale = new Vector3(originalScales[i].x * value, originalScales[i].y * value, originalScales[i].z * value);
        }
    }

    private void ScaleObstacles(float value)
    {
        if (level != GameManager.instance.currentLevel || droppingObjects || scalableObstacles.Length < 1) return;

        for (int i = 0; i < scalableObstacles.Length; i++)
        {
            scalableObstacles[i].localScale = new Vector3(originalObstacleScales[i].x * value, originalObstacleScales[i].y * value, originalObstacleScales[i].z * value);
        }
    }

    public void DropObjects()
    {
        enemySideShade.SetActive(false);
        droppingObjects = true;
        foreach (Transform playerObject in playerObjects)
        {
            playerObject.GetComponent<Rigidbody>().useGravity = true;
            playerObject.GetComponent<Rigidbody>().drag = 0;
        }

        invisibleFloor.SetActive(false);
    }

    void SaveOriginalStats()
    {
        // Save the original positions of the objects
        originalPositions = new Vector3[playerObjects.Length];
        originalScales = new Vector3[playerObjects.Length];
        if (scalableObstacles.Length > 0)
            originalObstacleScales = new Vector3[scalableObstacles.Length];

        //set initial positions and scales
        for (int i = 0; i < playerObjects.Length; i++)
        {
            originalPositions[i] = playerObjects[i].position;
            originalScales[i] = playerObjects[i].localScale;
        }
        if (scalableObstacles.Length > 0)
        {
            for (int i = 0; i < scalableObstacles.Length; i++)
            {
                originalObstacleScales[i] = scalableObstacles[i].localScale;
            }
        }
    }

    void MoveObjects(float sliderValue)
    {
        if (level != GameManager.instance.currentLevel) return;
        if (droppingObjects) return;
        // Move each object based on the clamped slider value
        for (int i = 0; i < playerObjects.Length; i++)
        {
            playerObjects[i].position = new Vector3(originalPositions[i].x + sliderValue, playerObjects[i].position.y, playerObjects[i].position.z);
        }
    }
    /*
        // Update is called once per frame
        void Update()
        {
            float lastScale = 0;
            if (objectScale == lastScale)
            {
                lastScale = objectScale;
                return;
            }

            foreach(Transform playerObject in playerObjects)
            {
                playerObject.GetComponent<Rigidbody>().mass = objectScale;
                playerObject.localScale = new Vector3(objectScale, objectScale, objectScale);
            }
        }*/
}
