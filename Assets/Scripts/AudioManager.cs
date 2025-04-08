using UnityEngine;
using UnityEngine.SceneManagement;
public class Audiomanager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip titlebgm;
    public AudioClip gamebgm;
    // 신이 늘어날 경우를 대비해서 여러 개의 브금 미리 불러놓기
    public static Audiomanager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            SceneManager.sceneLoaded += FindLoadedScene;
        }
        else Destroy(gameObject);
    }
    // 이건 지피티한테 물어봤어요. SceneManager.sceneLoaded 가 신이 로드되는 이벤트를 뜻하고,
    // +-가 '구독'이라는데 이벤트가 발생하면 += 어떤 행위를 한다~
    // 이런 느낌이라고 하네요.
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayBGM(SceneManager.GetActiveScene().name); // 시작 시 플레이 될 브금 선정!
    }

    void FindLoadedScene(Scene scene, LoadSceneMode mode)
    {
        PlayBGM(scene.name); // 씬 로드될 때마다 씬 이름값 불러와서 브금 교체
    }

    void PlayBGM(string currentscene)
    {
        
        if (currentscene == "StartScene")
        {
            audioSource.clip = titlebgm;
        }
        else
        {
            audioSource.clip = gamebgm;
        }
        audioSource.Stop();
        audioSource.volume = 0.1f;
        audioSource.Play();
    }
}
