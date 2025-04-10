using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatioFix : MonoBehaviour
{
    public float game_aspect_ratio = 760f / 1280f;
    private int width;
    private int height;
    private void Start()
    {
        width = Screen.width;
        height = Screen.height;
        // �ʱ�ȭ
        StartCoroutine(RatioChangeDetect());
    }
    IEnumerator RatioChangeDetect() // ������ �ð��� �������� ������ȭ ����
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            // 0.2�ʸ��� ����
            if (Screen.width != width || Screen.height != height)
                // 0.2�� �� �ʺ�,���� �� ���� �ٲ������ ����
            {
                width = Screen.width;
                height = Screen.height;
                ResizeHeight();
                // ���� ���� ����
            }
        }
    
    }
    public void ResizeHeight()
    {
        float currentAspect = (float)Screen.width / Screen.height;
        // ���� ȭ�� ���� ����
        if (Mathf.Abs(currentAspect - game_aspect_ratio) > 0.01f)
        // �ǵ��� ȭ������� ���̰� ������
        {
            int resize_height = Mathf.RoundToInt(Screen.width / game_aspect_ratio);
            Screen.SetResolution(Screen.width, resize_height, false);
        }
    }

}
