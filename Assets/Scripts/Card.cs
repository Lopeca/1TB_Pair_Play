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
        card_BTN.interactable = false;
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
        AudioPool.Instance.PlaySFX(1, 0.1f);
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
        AudioPool.Instance.PlaySFX(2, 0.1f);
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

    public void SortLayer(int num)
    {
        GetComponent<SortingGroup>().sortingOrder = num;
        GetComponentInChildren<Canvas>().sortingOrder = num;
    }

}
