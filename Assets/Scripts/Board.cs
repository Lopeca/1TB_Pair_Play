using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering;

public class Board : MonoBehaviour
{
    public Transform cardposition; // 카드가 놓일 위치
    public GameObject card; // 카드 프리팹

    private List<Card> cards = new List<Card>(); // 카드 리스트
    private Vector3 cardInitPos = Vector3.zero; // 카드 초기 위치
    void Start()
    {
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9 }; // 카드 번호 배열
        arr = arr.OrderBy(x => Random.Range(0f, 9f)).ToArray(); // 카드 번호 랜덤 섞기
        for (int i = 0; i < 20; i++)
        {
            GameObject go = Instantiate(card, this.transform); // 카드 프리팹 생성

            go.transform.parent = cardposition; // 카드 부모 설정

            float x = (i % 4) * 1.4f - 2.1f; // 카드 x 위치
            float y = (i % 5) * 1.4f - 4.0f; // 카드 y 위치
            go.transform.position = cardInitPos; // 카드 초기 위치 설정
            Card cardComponent = go.GetComponent<Card>(); // 카드 컴포넌트 가져오기
            cards.Add(cardComponent); // 카드 리스트에 추가
            cardComponent.Setting(arr[i]); // 카드 번호 설정
            cardComponent.designatedPosition = new Vector2(x, y); // 카드 지정 위치 설정
            Debug.Log("버튼 : " + cardComponent.mybutton);
            cardComponent.mybutton.interactable = false; // 카드 버튼 비활성화

            cardComponent.SortLayer(i); // 카드 정렬 레이어 설정
        }

        Gamemanager.instance.cardcount = arr.Length; // 카드 개수 설정
        StartCoroutine(PlayPlacementAnimation()); // 카드 배치 애니메이션 시작

    }

    // 카드 배치 애니메이션 코루틴
    IEnumerator PlayPlacementAnimation() // 카드 배치 애니메이션
    {
        yield return new WaitForSeconds(0.4f); // 애니메이션 시작 전 대기 시간
        float time = 0f; // 애니메이션 시간 초기화
        float duration = 1f; // 애니메이션 지속 시간
        float individualDuration = 0.3f; // 카드 개별 애니메이션 지속 시간
        cards = cards.OrderBy(x=>Random.Range(0,100)).ToList(); // 카드 리스트 랜덤 섞기
        while (time < duration) // 애니메이션 시간 동안 반복
        {
            time += Time.deltaTime; // 애니메이션 시간 증가
            for (int i = 0; i < cards.Count; i++) // 카드 개수만큼 반복
            {
                {
                    Card card = cards[i]; // 카드 가져오기
                    Vector3 from = card.transform.position; // 카드 현재 위치
                    Vector3 to = card.designatedPosition; // 카드 지정 위치
                    float t = (time -  (duration - individualDuration) / cards.Count * i) / individualDuration; // 카드 애니메이션 시간 계산
                    if (t > 1f) t = 1f; // 애니메이션 시간 1f 초과 시 1f로 설정

                    card.transform.position = Vector3.Lerp(cardInitPos, card.designatedPosition, 1 - (1 - t) * (1 - t)); // lerp 마지막 항은 ease-out 식
                }
             }

            yield return null; // 다음 프레임까지 대기
        }
        // 애니메이션 종료 후 카드 위치 설정
        cards.ForEach(card =>
        {
            card.transform.position = card.designatedPosition; // 카드 위치 설정
            card.mybutton.interactable = true; // 카드 버튼 활성화
        });


    }
}

