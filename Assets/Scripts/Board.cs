using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
#if UNITY_EDITOR
using UnityEditor.ShaderGraph.Internal;
#endif

public class Board : MonoBehaviour
{
    public Transform cardposition;
    public GameObject card;
    public RectTransform boardRect;

    private List<Card> cards = new List<Card>();
    private Vector3 cardInitPos = Vector3.zero;

    private LevelData levelData;
    void Start()
    {

        if (LevelDataCarrier.Instance.levelNum == -1)
        {
            LoadDemo();
        }

        else
        {
            LoadLevelByDataCarrier();
        }
        Destroy(boardRect.gameObject);
        StartCoroutine(PlayPlacementAnimation());

    }

    private void LoadLevelByDataCarrier()
    {
        string json = File.ReadAllText(LevelDataCarrier.Instance.GetLevelFilePath());
        levelData = JsonUtility.FromJson<LevelData>(json);

        Debug.Log("�ε� ����" + json);
        int rows = levelData.rows;
        int cols = levelData.cols;

        float cellWidth = boardRect.rect.width / cols;
        float cellHeight = boardRect.rect.height / rows;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (levelData.cardsPositions[i * cols + j] == 1)
                {
                    GameObject cardGO = Instantiate(card, transform);
                    Card cardComponent = cardGO.GetComponent<Card>();

                    cardComponent.card_BTN.interactable = false;

                    Vector3 offset = new Vector3(cellWidth * (j + 0.5f), cellHeight * (i + 0.5f), 0);
                    Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(null, boardRect.position + offset);
                    Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

                    if (i == 0 && j == 0) Debug.Log("첫번째 카드 포지션 : " + worldPos);
                    cardComponent.designatedPosition = worldPos + new Vector3(0, 0, 10);
                    cardComponent.transform.localScale = Vector3.one * levelData.cardScale;
                    cards.Add(cardComponent);
                }
            }
        }

        List<int> arr = GenerateNumbers();
        for (int i = 0; i < cards.Count; i++)
        {
            Card card = cards[i];
            card.Setting(arr[i]);
            card.SortLayer(i);
        }
        Gamemanager.Instance.cardcount = arr.Count;
    }

    private List<int> GenerateNumbers()
    {
        List<int> arr = new List<int>();

        int cardsAmount = cards.Count;

        // �� ����������� ¦ ��
        int needPair = cardsAmount / 2;

        for (int i = 0; i < needPair; i++)
        {
            arr.Add(i % 10);
            arr.Add(i % 10);
        }

        arr = arr.OrderBy(x => Random.Range(0f, 9f)).ToList();

        return arr;
    }



    private void LoadDemo()
    {
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9 };
        arr = arr.OrderBy(x => Random.Range(0f, 9f)).ToArray();
        for (int i = 0; i < 20; i++)
        {
            GameObject go = Instantiate(card, this.transform);

            go.transform.parent = cardposition;

            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i % 5) * 1.4f - 4.0f;
            go.transform.position = cardInitPos;
            Card cardComponent = go.GetComponent<Card>();
            cards.Add(cardComponent);
            cardComponent.Setting(arr[i]);
            cardComponent.designatedPosition = new Vector2(x, y);
            Debug.Log("��ư : " + cardComponent.card_BTN);
            cardComponent.card_BTN.interactable = false;

            cardComponent.SortLayer(i);
        }

        Gamemanager.Instance.cardcount = arr.Length;
    }

    IEnumerator PlayPlacementAnimation()
    {
        yield return new WaitForSeconds(0.4f);

        float time = 0f;
        float duration = 1f;
        float individualDuration = 0.3f;
        AudioPool.Instance.PlaySFX(3, 0.1f);
        cards = cards.OrderBy(x => Random.Range(0, 100)).ToList();
        while (time < duration)
        {
            time += Time.deltaTime;
            for (int i = 0; i < cards.Count; i++)
            {
                {
                    Card card = cards[i];

                    float t = (time - (duration - individualDuration) / cards.Count * i) / individualDuration;
                    if (t > 1) t = 1;
                    card.transform.position = Vector3.Lerp(cardInitPos, card.designatedPosition, 1 - (1 - t) * (1 - t)); // lerp ������ ���� ease-out ��
                }
            }

            yield return null;
        }

        cards.ForEach(card =>
        {
            card.transform.position = card.designatedPosition;
            card.card_BTN.interactable = true;
        });
    }
}

