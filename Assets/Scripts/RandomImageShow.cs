using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomImageShow : MonoBehaviour
{
    public GameObject[] imagePrefabs;       // �̹��� ������ 5��
    public RectTransform canvasTransform;   // �θ� ĵ���� (UI��)
    public float time = 0.8f;               // ���� �ֱ� (��)

    private int lastIndex = -1;             // ���������� ���� �̹��� �ε���

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
        AudioPool.Instance.PlayBGM(0, 0.2f);
        StartCoroutine(SpawnLoop());
    }

    void ShowImageWithoutRepeat()
    {
        // ������ �ε����� ������, ������ �ٸ� �͸�
        int index;
        do
        {
            index = Random.Range(0, imagePrefabs.Length);
        } 
        while (index == lastIndex);

        lastIndex = index;
        GameObject prefab = imagePrefabs[index];

        //���� ���� ����
        float x = Random.Range(-250f, 250f);
        float y = Random.Range(-100f, 100f);
        Vector2 anchoredPos = new Vector2(x, y);

        //���� ��ġ�� ���� ����
        GameObject newImage = Instantiate(prefab, canvasTransform);
        RectTransform rect = newImage.GetComponent<RectTransform>();
        rect.anchoredPosition = anchoredPos;

        //�ı�
        Destroy(newImage, time);
    }
}
