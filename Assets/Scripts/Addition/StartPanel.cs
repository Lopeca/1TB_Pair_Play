using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanel : MonoBehaviour
{
    // (��ȭ) ���� �������� ��ư�� ���� ��������� ���ư��� �ִ� �� �ϴ� ���ΰڽ��ϴ�
    public GameObject levelSelectPanel;

    public void OnClickLevelSelectMenu()
    {
        MenuManager.Instance.OpenMenu(levelSelectPanel);
    }


}
