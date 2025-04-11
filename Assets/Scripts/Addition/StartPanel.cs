using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPanel : MonoBehaviour
{
    // (승화) 데모 스테이지 버튼이 먼저 만들어져서 돌아가고 있던 건 일단 냅두겠습니다
    public GameObject levelSelectPanel;

    public void OnClickDemoGameBtn()
    {
        
        AudioPool.Instance.PlayBGM(1, 0.2f);
        SceneManager.LoadScene("MainScene");
    }
    public void OnClickLevelSelectMenu()
    {
        MenuManager.Instance.OpenMenu(levelSelectPanel);
    }
}
