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
    // ��ŵ ��ư ���� ���� ����
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
    // ���ν� ���� ��ư. ��ư �Ҹ� �� ���� ����� �ִ� �Լ�
    IEnumerator ToMainScene()
    {
        to_mainscene_btn.interactable = false;
        AudioPool.Instance.PlaySFX(0, 0.05f);
        yield return new WaitForSeconds(0.4f);
        to_mainscene_btn.interactable = true;
        AudioPool.Instance.PlayBGM(1, 0.02f);
        SceneManager.LoadScene("MainScene");
    }
    // ���۽� ���� ��ư. ��ư �Ҹ� �� ���� ����� �ִ� �Լ�
    IEnumerator ToStartScene()
    {
        to_startscene_btn.interactable = false;
        AudioPool.Instance.PlaySFX(0, 0.05f);
        yield return new WaitForSeconds(0.4f);
        to_startscene_btn.interactable = true;
        AudioPool.Instance.PlayBGM(0,0.01f);
        SceneManager.LoadScene("StartScene");
    }

    // �ٵ� �� �س��� ������ ���� ������Ŵ����� DonDestroy �صΰ� Ǯ���صּ�
    // �� �ٷ� �Ѿ�� �Ҹ� �� ������� �ʳ���...? �� �̷��� �־���
    // ��ŵ��ư ����� Addlistener�� �� �ʿ� ���� �ڷ�ƾ�� �� �ᵵ �ƴ� �� �Ƴ�??

}


