using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class Card : MonoBehaviour
{
    public int idx = 0;
    public GameObject front;
    public GameObject back;
    public Animator Anim;
    public Button card_BTN;
    public SpriteRenderer frontimage;
    public Vector3 designatedPosition;

    void Start()
    {
        // mybutton = GetComponentInChildren<Button>(); // ��ư ���� �Ҵ����� �ʰ� Card �ڽĿ� �ִ� ��ư �ڵ���Ī
        //mybutton.interactable = true; // Ÿ�ӿ����� ���� ���� �� ī�尡 ������ �ʵ��� ��ȣ�ۿ� ���� on,off ����
        
    }

    void Update()
    {
        if (Gamemanager.Instance.isTimeover)
        {
            Anim.SetBool("timeover", true); // Ÿ�ӿ��� �� ī�� ��鸮�� �ִϸ��̼��� �����ϱ� ���� : �ִϸ��̼� ��Ʈ�ѷ� Ȯ�κ�Ź
            card_BTN.interactable = false; // Ÿ�ӿ��� �� ��� ī���� ��ư�� ������ �ʵ��� ��ư ��ȣ�ۿ� off
        }
    }
    public void Setting(int number)
    {
        idx = number;
        frontimage.sprite = Resources.Load<Sprite>($"1TB_{idx}");
    }

    public void OpenCard()
    {
        Anim.SetBool("Isopen", true);
        AudioPool.Instance.PlaySFX(1, 0.3f);
        front.SetActive(true);
        back.SetActive(false);
        if (Gamemanager.Instance.firstcard == null)
        {
            Gamemanager.Instance.firstcard = this;
        }
        else
        {
            Gamemanager.Instance.secondcard = this;
            Gamemanager.Instance.IsSameCard();
        }
    }

    public void DestroyCard()
    {
        AudioPool.Instance.PlaySFX(2, 0.3f);
        Invoke("DestroyTimeDelay", 1.0f);
    }

    public void CloseCard()
    {
        frontimage.color = new Color(1f, 0.6f, 0.6f, 1f);
        Invoke("CloseTimeDelay", 1.0f);
    }

    public void DestroyTimeDelay()
    {
        Destroy(gameObject);
    }

    public void CloseTimeDelay()
    {
        frontimage.color = Color.white;
        Anim.SetBool("Isopen", false);
        front.SetActive(false);
        back.SetActive(true);
    }

    public void SortLayer(int num)
    {
        GetComponent<SortingGroup>().sortingOrder = num;
        GetComponentInChildren<Canvas>().sortingOrder = num;
    }

}
