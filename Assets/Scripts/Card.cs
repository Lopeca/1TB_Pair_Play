using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class Card : MonoBehaviour
{
    AudioSource AudioSource;
    public AudioClip clip;
    public int idx = 0;
    public GameObject front;
    public GameObject back;
    public Animator Anim;
    public Button mybutton;
    public SpriteRenderer frontimage;
    public Vector3 designatedPosition;

    void Start()
    {
        // mybutton = GetComponentInChildren<Button>(); // 버튼 직접 할당하지 않고 Card 자식에 있는 버튼 자동서칭
        mybutton.interactable = true; // 타임오버나 게임 종료 시 카드가 눌리지 않도록 상호작용 여부 on,off 관리
        AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (GameManager.instance.timeover)
        {
            Anim.SetBool("timeover", true); // 타임오버 시 카드 흔들리는 애니메이션을 정지하기 위함 : 애니메이션 컨트롤러 확인부탁
            mybutton.interactable = false; // 타임오버 시 즉시 카드의 버튼이 눌리지 않도록 버튼 상호작용 off
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
        if (GameManager.instance.firstcard == null)
        {
            GameManager.instance.firstcard = this;
        }
        else
        {
            GameManager.instance.secondcard = this;
            GameManager.instance.IsSameCard();
        }
    }

    public void DestroyCard()
    {
        Invoke("DestroyTimeDelay", 1.0f);
    }

    public void CloseCard()
    {
        frontimage.color = new Color(1, 0.66f, 0.66f);
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
        frontimage.color = Color.white;
    }
    // firstcard가 비었다면 firstcard에 내 정보를 넘겨준다.
    // firstcard가 비어 있지 않다면 secondcard에 내 정보를 넘겨주고 matched함수를 불러온다.

    public void SortLayer(int num)
    {
        GetComponent<SortingGroup>().sortingOrder = num;
        GetComponentInChildren<Canvas>().sortingOrder = num;
    }

}
