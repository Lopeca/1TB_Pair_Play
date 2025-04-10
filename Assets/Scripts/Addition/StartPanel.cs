using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPanel : MonoBehaviour
{
    // (��ȭ) ���� �������� ��ư�� ���� ��������� ���ư��� �ִ� �� �ϴ� ���ΰڽ��ϴ�
    public GameObject levelSelectPanel;

    public void OnClickDemoGameBtn()
    {
        
        AudioPool.Instance.PlayBGM(1, 0.02f);
        SceneManager.LoadScene("MainScene");
    }
    public void OnClickLevelSelectMenu()
    {
        MenuManager.Instance.OpenMenu(levelSelectPanel);
    }
}
