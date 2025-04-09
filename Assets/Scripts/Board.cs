using System.Collections;
using System.Collections.Generic;
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

    private List<Card> cards = new List<Card>();
    private Vector3 cardInitPos = Vector3.zero;
    void Start()
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
            Debug.Log("버튼 : " + cardComponent.mybutton);
            cardComponent.mybutton.interactable = false;

            cardComponent.SortLayer(i);
        }

        Gamemanager.instance.cardcount = arr.Length;
        StartCoroutine(PlayPlacementAnimation());

    }

    IEnumerator PlayPlacementAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        float time = 0f;
        float duration = 1f;
        float individualDuration = 0.3f;
        cards = cards.OrderBy(x=>Random.Range(0,100)).ToList();
        while (time < duration)
        {
            time += Time.deltaTime;
            for (int i = 0; i < cards.Count; i++) {
                {
                    Card card = cards[i];
                    Vector3 from = card.transform.position;
                    Vector3 to = card.designatedPosition;
                    float t = (time -  (duration - individualDuration) / cards.Count * i) / individualDuration;
                    if (t > 1f) t = 1f;
                   
                    card.transform.position = Vector3.Lerp(cardInitPos, card.designatedPosition, 1 - (1 - t) * (1 - t)); // lerp 마지막 항은 ease-out 식
                }
             }

            yield return null;
        }

        cards.ForEach(card =>
        {
            card.transform.position = card.designatedPosition;
            card.mybutton.interactable = true;
        });


    }
}

