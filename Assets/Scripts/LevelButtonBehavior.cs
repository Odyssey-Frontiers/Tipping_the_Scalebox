using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButtonBehavior : MonoBehaviour
{
    public int level;

    // Start is called before the first frame update
    void Start()
    {
        if (level <= (RunningManager.instance.lastLevelCompleted + 1))
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
