using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering;

public class Board : MonoBehaviour
{
    public Transform cardposition; // ī�尡 ���� ��ġ
    public GameObject card; // ī�� ������

    private List<Card> cards = new List<Card>(); // ī�� ����Ʈ
    private Vector3 cardInitPos = Vector3.zero; // ī�� �ʱ� ��ġ
    void Start()
    {
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9 }; // ī�� ��ȣ �迭
        arr = arr.OrderBy(x => Random.Range(0f, 9f)).ToArray(); // ī�� ��ȣ ���� ����
        for (int i = 0; i < 20; i++)
        {
            GameObject go = Instantiate(card, this.transform); // ī�� ������ ����

            go.transform.parent = cardposition; // ī�� �θ� ����

            float x = (i % 4) * 1.4f - 2.1f; // ī�� x ��ġ
            float y = (i % 5) * 1.4f - 4.0f; // ī�� y ��ġ
            go.transform.position = cardInitPos; // ī�� �ʱ� ��ġ ����
            Card cardComponent = go.GetComponent<Card>(); // ī�� ������Ʈ ��������
            cards.Add(cardComponent); // ī�� ����Ʈ�� �߰�
            cardComponent.Setting(arr[i]); // ī�� ��ȣ ����
            cardComponent.designatedPosition = new Vector2(x, y); // ī�� ���� ��ġ ����
            Debug.Log("��ư : " + cardComponent.mybutton);
            cardComponent.mybutton.interactable = false; // ī�� ��ư ��Ȱ��ȭ

            cardComponent.SortLayer(i); // ī�� ���� ���̾� ����
        }

        Gamemanager.instance.cardcount = arr.Length; // ī�� ���� ����
        StartCoroutine(PlayPlacementAnimation()); // ī�� ��ġ �ִϸ��̼� ����

    }

    // ī�� ��ġ �ִϸ��̼� �ڷ�ƾ
    IEnumerator PlayPlacementAnimation() // ī�� ��ġ �ִϸ��̼�
    {
        yield return new WaitForSeconds(0.4f); // �ִϸ��̼� ���� �� ��� �ð�
        float time = 0f; // �ִϸ��̼� �ð� �ʱ�ȭ
        float duration = 1f; // �ִϸ��̼� ���� �ð�
        float individualDuration = 0.3f; // ī�� ���� �ִϸ��̼� ���� �ð�
        cards = cards.OrderBy(x=>Random.Range(0,100)).ToList(); // ī�� ����Ʈ ���� ����
        while (time < duration) // �ִϸ��̼� �ð� ���� �ݺ�
        {
            time += Time.deltaTime; // �ִϸ��̼� �ð� ����
            for (int i = 0; i < cards.Count; i++) // ī�� ������ŭ �ݺ�
            {
                {
                    Card card = cards[i]; // ī�� ��������
                    Vector3 from = card.transform.position; // ī�� ���� ��ġ
                    Vector3 to = card.designatedPosition; // ī�� ���� ��ġ
                    float t = (time -  (duration - individualDuration) / cards.Count * i) / individualDuration; // ī�� �ִϸ��̼� �ð� ���
                    if (t > 1f) t = 1f; // �ִϸ��̼� �ð� 1f �ʰ� �� 1f�� ����

                    card.transform.position = Vector3.Lerp(cardInitPos, card.designatedPosition, 1 - (1 - t) * (1 - t)); // lerp ������ ���� ease-out ��
                }
             }

            yield return null; // ���� �����ӱ��� ���
        }
        // �ִϸ��̼� ���� �� ī�� ��ġ ����
        cards.ForEach(card =>
        {
            card.transform.position = card.designatedPosition; // ī�� ��ġ ����
            card.mybutton.interactable = true; // ī�� ��ư Ȱ��ȭ
        });


    }
}

