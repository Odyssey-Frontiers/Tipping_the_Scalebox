using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class WinLoseTriggerBehavior : MonoBehaviour
{
    [SerializeField] private bool winTrigger;
    [SerializeField] private bool lastLevel;

    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject loseText;
    [SerializeField] private GameObject endingText;

    public AudioSource winAudioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (lastLevel && winTrigger)
        {
            endingText.SetActive(true);
            GameManager.instance.CompleteLevel();
            winAudioSource.Stop();

            winAudioSource.Play();
        }
        else if (winTrigger)
        {
            winText.SetActive(true);
            GameManager.instance.CompleteLevel();
            winAudioSource.Stop();

            winAudioSource.Play();
        }
        else
            loseText.SetActive(true);

        transform.parent.gameObject.SetActive(false);
    }
}
