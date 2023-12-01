using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class HitEffects : MonoBehaviour
{
    public AudioSource hitSFX;
    public int level;

    private void OnCollisionEnter(Collision collision)
    {
        if ( (GameManager.instance.currentLevel == level) && GameManager.instance.inLevel)
        {
            hitSFX.Play();
        }
    }
}
