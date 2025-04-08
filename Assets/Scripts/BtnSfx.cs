using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnSfx : MonoBehaviour
{
    public AudioClip clickSfx;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.volume = 1.0f;
    }

    public void PlayClickSound()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(clickSfx);
        }
        Invoke("Retry", 0.5f);
    }
    private void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void PlayClickSound2()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(clickSfx);
        }
        Invoke("StartScene", 0.5f);
    }
    private void StartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void PlayClickSound3()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(clickSfx);
        }
    }
}
