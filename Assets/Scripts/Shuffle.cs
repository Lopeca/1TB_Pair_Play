using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuffle : MonoBehaviour
{
    public AudioClip ShuffleSfx;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null )
        {
            audioSource= gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
    }
    // Start is called before the first frame update
    private void Start()
    {
        Invoke("PlayShuffleSound", 0.4f);
    }
    private void PlayShuffleSound()
    {
        audioSource.PlayOneShot(ShuffleSfx);
    }
}
