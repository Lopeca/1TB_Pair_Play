using UnityEngine;
using UnityEngine.UI;


public class card : MonoBehaviour
{
    AudioSource AudioSource;
    public AudioClip clip;
    public int idx = 0;
    public GameObject front;
    public GameObject back;
    public Animator Anim;
    public Button mybutton;
    public SpriteRenderer frontimage;

    void Start()
    {
        mybutton = GetComponentInChildren<Button>(); // ��ư ���� �Ҵ����� �ʰ� Card �ڽĿ� �ִ� ��ư �ڵ���Ī
        mybutton.interactable = true; // Ÿ�ӿ����� ���� ���� �� ī�尡 ������ �ʵ��� ��ȣ�ۿ� ���� on,off ����
        AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Gamemanager.instance.timeover)
        {
            Anim.SetBool("timeover", true); // Ÿ�ӿ��� �� ī�� ��鸮�� �ִϸ��̼��� �����ϱ� ���� : �ִϸ��̼� ��Ʈ�ѷ� Ȯ�κ�Ź
            mybutton.interactable = false; // Ÿ�ӿ��� �� ��� ī���� ��ư�� ������ �ʵ��� ��ư ��ȣ�ۿ� off
        }
    }
    public void Setting(int number)
    {
        idx = number;
        frontimage.sprite = Resources.Load<Sprite>($"1TB_{idx}");

    }

    public void OpenCard()
    {
        AudioSource.PlayOneShot(clip);
        Anim.SetBool("Isopen", true);
        front.SetActive(true);
        back.SetActive(false);
        if (Gamemanager.instance.firstcard == null)
        {
            Gamemanager.instance.firstcard = this;
        }
        else
        {
            Gamemanager.instance.secondcard = this;
            Gamemanager.instance.IsSameCard();
        }
    }

    public void DestroyCard()
    {
        Invoke("DestroyTimeDelay", 1.0f);
    }

    public void CloseCard()
    {
        Invoke("CloseTimeDelay", 1.0f);
    }

    public void DestroyTimeDelay()
    {
        Destroy(gameObject);
    }

    public void CloseTimeDelay()
    {
        Anim.SetBool("Isopen", false);
        front.SetActive(false);
        back.SetActive(true);
    }
    // firstcard�� ����ٸ� firstcard�� �� ������ �Ѱ��ش�.
    // firstcard�� ��� ���� �ʴٸ� secondcard�� �� ������ �Ѱ��ְ� matched�Լ��� �ҷ��´�.

}
