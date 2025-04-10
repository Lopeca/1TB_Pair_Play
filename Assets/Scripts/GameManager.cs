using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    // ���� ����� ����
    AudioSource AudioSource;
    public AudioClip destroyclip;
    //���� �ð� ���� - MainScene �ε� �� �ð� 0�ʷ� �ʱ�ȭ
    float timer_time = 20f;
    public Text timeTXT;
    //���� ī�� ������ ���� ����
    public int cardcount = 20;
    public Card firstcard;
    public Card secondcard;
    // ���� ���� ���� ���� : Ÿ�ӿ��� �ÿ� Ŭ���� ���� UI ��ȭ�� ���� �� ���� ����
    public GameObject endtext;
    public GameObject EngingIMG;
    public bool timeover = false;

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
            timer_time -= Time.unscaledDeltaTime; // ī�� �ִϸ��̼��̳� UI�� �ִϸ��̼� ���� �� Ÿ�̸Ӱ� ���絵 �����ϱ� ���� unscaled ���
            timer_time = Mathf.Max(timer_time, 0f); // ���� ����
            timeTXT.text = $"���� �ð�:\n{timer_time.ToString("N2")}"; // ȭ�鿡�� ���� �ð��� ǥ��!
        }

        if (timer_time <= 0f && !timeover) // Ÿ�ӿ��� �� ����
        {
            timeover = true;
            endtext.SetActive(true);
            EngingIMG.SetActive(true);
        }
    }
    public void OnCardMatched()
    {
        timer_time += 5f; // �ð� ���ʽ�
    }

    public void IsSameCard()
    {
        if (firstcard.idx == secondcard.idx)
        {
            AudioSource.PlayOneShot(destroyclip);
            firstcard.DestroyCard();
            secondcard.DestroyCard();
            cardcount -= 2;
            OnCardMatched();
        }
        else
        {
            firstcard.CloseCard();
            secondcard.CloseCard();
        }
        firstcard = null;
        secondcard = null;

        if (cardcount == 0) // ���� Ŭ���� �� ����
        {
            SceneManager.LoadScene("EndScene");
        }
    }
}
