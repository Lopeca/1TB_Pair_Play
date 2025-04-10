using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    //���� �ð� ���� - MainScene �ε� �� �ð� 0�ʷ� �ʱ�ȭ
    float timer_time = 20f;
    public Text timer_txt;
    //���� ī�� ������ ���� ����
    public int cardcount = 20;
    public Card firstcard;
    public Card secondcard;
    // ���� ���� ���� ���� : Ÿ�ӿ��� �ÿ� Ŭ���� ���� UI ��ȭ�� ���� �� ���� ����
    public GameObject timeover_txt;
    public GameObject timeover_IMG;
    public bool isTimeover = false;

    // �̱���
    public static Gamemanager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    void Start()
    {
        Time.timeScale = 1.0f;        
    }

    void Update()
    {
        if (!(cardcount == 0 || isTimeover)) // ī��ī��Ʈ�� 0�� �ǰų� Ÿ�ӿ����� �Ǹ� Ÿ�̸� ����
        {
            timer_time -= Time.unscaledDeltaTime; // ī�� �ִϸ��̼��̳� UI�� �ִϸ��̼� ���� �� Ÿ�̸Ӱ� ���絵 �����ϱ� ���� unscaled ���
            timer_time = Mathf.Max(timer_time, 0f); // ���� ����
            timer_txt.text = $"���� �ð�:\n{timer_time.ToString("N2")}"; // ȭ�鿡�� ���� �ð��� ǥ��!
        }

        if (timer_time <= 0f && !isTimeover) // Ÿ�ӿ��� �� ����
        {
            isTimeover = true;
            timer_txt.text = "<color=red>Ÿ�� ����!</color>";
            timeover_txt.SetActive(true);
            timeover_IMG.SetActive(true);
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
            AudioPool.Instance.PlayBGM(2, 0.01f);
            SceneManager.LoadScene("EndScene");
        }
    }
}
