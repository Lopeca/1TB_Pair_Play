using System.Data;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    // ���� ����� ����
    AudioSource AudioSource;
    public AudioClip destroyclip;
    //���� �ð� ���� - MainScene �ε� �� �ð� 0�ʷ� �ʱ�ȭ
    float timer_time = 0.0f;
    public Text timeTXT;
    //���� ī�� ������ ���� ����
    public int cardcount = 0;
    public Card firstcard;
    public Card secondcard;
    // ���� ���� ���� ���� : Ÿ�ӿ��� �ÿ� Ŭ���� ���� UI ��ȭ�� ���� �� ���� ����
    public GameObject endtext;
    public GameObject EngingIMG;
    public bool timeover = false;
    public float clickcounter = 0;

    // �̱���
    public static Gamemanager instance;
    
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        Time.timeScale = 1.0f;
    }

 
    void Update()
    {
        if (!(cardcount == 0 || timeover)) // ī��ī��Ʈ�� 0�� �ǰų� Ÿ�ӿ����� �Ǹ� Ÿ�̸� ����
        {
            timer_time += Time.unscaledDeltaTime; // ī�� �ִϸ��̼��̳� UI�� �ִϸ��̼� ���� �� Ÿ�̸Ӱ� ���絵 �����ϱ� ���� unscaled ���
            timeTXT.text = $"���� �ð�:\n{(70 - timer_time).ToString("N2")}"; // ȭ�鿡�� ���� �ð��� ǥ��!
        }

        if (timer_time > 70) // Ÿ�ӿ��� �� ����
        {
            timeover = true;
            timeTXT.text = "<color=red>Ÿ�� ����!</color>";
            endtext.SetActive(true);
            EngingIMG.SetActive(true);
        }

        if (clickcounter >= 10) // ��ŵ �� ����
        {
            cardcount = 0;
            timeTXT.text = "<color=blue>��ŵ!</color>";
            endtext.SetActive(true);
            EngingIMG.SetActive(true);
        }
    }
    public void IsSameCard()
    {
        if (firstcard.idx == secondcard.idx)
        {
            AudioSource.PlayOneShot(destroyclip);
            firstcard.DestroyCard();
            secondcard.DestroyCard();
            cardcount -= 2;
        }
        else
        {
            firstcard.CloseCard();
            secondcard.CloseCard();
        }
        firstcard = null;
        secondcard = null;

        if (cardcount == 0) // ���� �븻 Ŭ���� �� ����
        {
            timeTXT.text = "<color=blue>Ŭ����!</color>";
            EngingIMG.SetActive(true);
            endtext.SetActive(true);
        }
    }
}
