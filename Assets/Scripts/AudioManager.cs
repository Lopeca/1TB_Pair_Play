using UnityEngine;

public class Audiomanager : MonoBehaviour
{
    public static Audiomanager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    AudioSource audioSource;
    public AudioClip bgm;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = this.bgm;
        audioSource.volume = 0.1f; // 이상하게 유니티에서 소리가 커서 볼륨 낮춤
        audioSource.Play();
    }
}
