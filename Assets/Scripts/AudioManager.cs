using UnityEngine;
using UnityEngine.SceneManagement;
public class Audiomanager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip titlebgm;
    public AudioClip gamebgm;
    // ���� �þ ��츦 ����ؼ� ���� ���� ��� �̸� �ҷ�����
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
    // �̰� ����Ƽ���� ����þ��. SceneManager.sceneLoaded �� ���� �ε�Ǵ� �̺�Ʈ�� ���ϰ�,
    // +-�� '����'�̶�µ� �̺�Ʈ�� �߻��ϸ� += � ������ �Ѵ�~
    // �̷� �����̶�� �ϳ׿�.
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayBGM(SceneManager.GetActiveScene().name); // ���� �� �÷��� �� ��� ����!
    }

    void FindLoadedScene(Scene scene, LoadSceneMode mode)
    {
        PlayBGM(scene.name); // �� �ε�� ������ �� �̸��� �ҷ��ͼ� ��� ��ü
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
