using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardSettingPanel : MonoBehaviour
{
    public int width = 1;
    public int height = 1;

    int maxSize = 100;
    public Text widthText;
    public Text heightText;

    public Text errorText;
    public void TrySetBoardSize(SizeType type, int delta)
    {
        int nextWidth = width;
        int nextHeight = height;

        if (type == SizeType.Width)
            nextWidth += delta;
        else
            nextHeight += delta;

        if (nextWidth < 1 || nextHeight < 1)
        {
            return;
        }

        if(nextWidth * nextHeight > maxSize)
        {
            errorText.text = "더 큰 보드는 설정할 수 없습니다";
            errorText.gameObject.SetActive(true);
            return;
        }

        width = nextWidth;
        widthText.text = width.ToString();
        height = nextHeight;
        heightText.text = height.ToString();   
        
        errorText.gameObject.SetActive(false);
    }

    public void OnClickDecide()
    {
        LevelEditor.Instance.InitializeBySetting(width, height);
        gameObject.SetActive(false);
    }
}
