using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomImageShow : MonoBehaviour
{
    public GameObject[] imagePrefabs;       // 이미지 프리팹 5개
    public RectTransform canvasTransform;   // 부모 캔버스 (UI용)
    public float time = 0.8f;               // 생성 주기 (초)

    private int lastIndex = -1;             // 마지막으로 나온 이미지 인덱스

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            ShowImageWithoutRepeat();
            yield return new WaitForSeconds(time);
        }
    }

    void OnEnable()
    {
        Debug.Log("랜덤이미지 실행");
        StartCoroutine(SpawnLoop());
    }

    void ShowImageWithoutRepeat()
    {
        // 무작위 인덱스를 고르되, 이전과 다른 것만
        int index;
        do
        {
            index = Random.Range(0, imagePrefabs.Length);
        } 
        while (index == lastIndex);

        lastIndex = index;
        GameObject prefab = imagePrefabs[index];

        //생성 범위 설정
        float x = Random.Range(-250f, 250f);
        float y = Random.Range(-100f, 100f);
        Vector2 anchoredPos = new Vector2(x, y);

        //램덤 위치에 램덤 생성
        GameObject newImage = Instantiate(prefab, canvasTransform);
        RectTransform rect = newImage.GetComponent<RectTransform>();
        rect.anchoredPosition = anchoredPos;

        //파괴
        Destroy(newImage, time);
    }
}
