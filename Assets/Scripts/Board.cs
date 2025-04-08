using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{


    public Transform cardposition;
    public GameObject card;
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
            go.transform.position = new Vector2(x, y);
            go.GetComponent<Card>().Setting(arr[i]);

        }

        Gamemanager.instance.cardcount = arr.Length;
    }

}

