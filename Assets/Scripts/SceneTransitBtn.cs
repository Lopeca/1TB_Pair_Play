using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitBtn : MonoBehaviour
{
    public Button to_endscene_btn;
    public Button to_mainscene_btn;
    public Button to_startscene_btn;
    public bool is_counting = false;
    public float clickcount = 0f;
    // 스킵 버튼 관련 변수 선언
    void Start()
    {
        to_endscene_btn?.onClick.AddListener(SkipCardMatching);
        to_mainscene_btn?.onClick.AddListener(()=>StartCoroutine(ToMainScene()));
        to_startscene_btn?.onClick.AddListener(()=>StartCoroutine(ToStartScene()));
    }

    void SkipCardMatching()
    {
        clickcount += 1f;
        if (!is_counting) StartCoroutine(ClicktoSkip());
    }

    IEnumerator ClicktoSkip()
    {
        is_counting = true;
        clickcount = 0f;
        while (clickcount < 10f)
        {
            clickcount -= Time.deltaTime;
            clickcount = Mathf.Clamp(clickcount, 0f, 11f);
            yield return null;
        }

        if (clickcount >= 10f)
        {
            is_counting = false;
            AudioPool.Instance.PlayBGM(2, 0.01f);
            SceneManager.LoadScene("EndScene");
        }
    }
    // 메인신 가는 버튼. 버튼 소리 날 동안 대기해 주는 함수
    IEnumerator ToMainScene()
    {
        to_mainscene_btn.interactable = false;
        AudioPool.Instance.PlaySFX(0, 0.05f);
        yield return new WaitForSeconds(0.4f);
        to_mainscene_btn.interactable = true;
        AudioPool.Instance.PlayBGM(1, 0.02f);
        SceneManager.LoadScene("MainScene");
    }
    // 시작신 가는 버튼. 버튼 소리 날 동안 대기해 주는 함수
    IEnumerator ToStartScene()
    {
        to_startscene_btn.interactable = false;
        AudioPool.Instance.PlaySFX(0, 0.05f);
        yield return new WaitForSeconds(0.4f);
        to_startscene_btn.interactable = true;
        AudioPool.Instance.PlayBGM(0,0.01f);
        SceneManager.LoadScene("StartScene");
    }

    // 근데 다 해놓고 생각해 보니 오디오매니저를 DonDestroy 해두고 풀링해둬서
    // 신 바로 넘어가도 소리 잘 재생되지 않나요...? 왜 이러고 있었지
    // 스킵버튼 말고는 Addlistener도 할 필요 없고 코루틴도 안 써도 됐던 거 아냐??

}


