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
        // 초기화
        StartCoroutine(RatioChangeDetect());
    }
    IEnumerator RatioChangeDetect() // 설정된 시간초 간격으로 비율변화 감지
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            // 0.2초마다 감지
            if (Screen.width != width || Screen.height != height)
                // 0.2초 전 너비,높이 중 뭐라도 바뀌었으면 갱신
            {
                width = Screen.width;
                height = Screen.height;
                ResizeHeight();
                // 비율 고정 진행
            }
        }
    
    }
    public void ResizeHeight()
    {
        float currentAspect = (float)Screen.width / Screen.height;
        // 현재 화면 비율 측정
        if (Mathf.Abs(currentAspect - game_aspect_ratio) > 0.01f)
        // 의도된 화면비율과 차이가 있으면
        {
            int resize_height = Mathf.RoundToInt(Screen.width / game_aspect_ratio);
            Screen.SetResolution(Screen.width, resize_height, false);
        }
    }

}
