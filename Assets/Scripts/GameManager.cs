using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    // 이하 오디오 관련
    AudioSource AudioSource;
    public AudioClip destroyclip;
    //이하 시간 관련 - MainScene 로드 시 시간 0초로 초기화
    float timer_time = 20f;
    public Text timeTXT;
    //이하 카드 뒤집기 로직 관련
    public int cardcount = 20;
    public Card firstcard;
    public Card secondcard;
    // 이하 게임 엔딩 관련 : 타임오버 시와 클리어 시의 UI 변화는 논의 후 제작 예정
    public GameObject endtext;
    public GameObject EngingIMG;
    public bool timeover = false;

    // 싱글톤
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
        if (!(cardcount == 0 || timeover)) // 카드카운트가 0이 되거나 타임오버가 되면 타이머 정지
        {
            timer_time -= Time.unscaledDeltaTime; // 카드 애니매이션이나 UI에 애니매이션 넣을 시 타이머가 멈춰도 진행하기 위해 unscaled 사용
            timer_time = Mathf.Max(timer_time, 0f); // 음수 방지
            timeTXT.text = $"남은 시간:\n{timer_time.ToString("N2")}"; // 화면에는 남은 시간을 표시!
        }

        if (timer_time <= 0f && !timeover) // 타임오버 시 엔딩
        {
            timeover = true;
            endtext.SetActive(true);
            EngingIMG.SetActive(true);
        }
    }
    public void OnCardMatched()
    {
        timer_time += 5f; // 시간 보너스
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

        if (cardcount == 0) // 게임 클리어 시 엔딩
        {
            SceneManager.LoadScene("EndScene");
        }
    }
}
