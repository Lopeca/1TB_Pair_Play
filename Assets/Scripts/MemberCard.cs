using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemberCard : MonoBehaviour
{
    // UI Button 컴포넌트 (Inspector에서 할당)
    public Button nextSpriteButton;      // 다음 이미지로 변경하는 버튼
    public Button previousSpriteButton;  // 이전 이미지로 변경하는 버튼

    // 스프라이트 렌더러 (버튼 클릭 시 이미지가 변경될 대상)
    public SpriteRenderer spriteRenderer;

    // 스프라이트 파일의 시작 및 종료 인덱스
    public int startSpriteIndex = 1;
    public int endSpriteIndex = 5;

    private int currentSpriteIndex;

    // Start is called before the first frame update
    void Start()
    {
        // 초기 이미지 인덱스 설정
        currentSpriteIndex = startSpriteIndex;
        AudioPool.Instance.PlayBGM(0, 0.1f);
        UpdateSprite();

        // 버튼 이벤트 리스너 등록
        if (nextSpriteButton != null)
        {
            nextSpriteButton.onClick.AddListener(NextSprite);
        }
        if (previousSpriteButton != null)
        {
            previousSpriteButton.onClick.AddListener(PreviousSprite);
        }
    }
    // 다음 이미지로 전환하는 메소드
    public void NextSprite()
    {
        Debug.Log("NextSprite() 호출됨. 현재 인덱스: " + currentSpriteIndex);
        currentSpriteIndex++;
        AudioPool.Instance.PlaySFX(0, 0.1f);
        if (currentSpriteIndex > endSpriteIndex)
        {
            currentSpriteIndex = startSpriteIndex;
        }
        UpdateSprite();
    }

    // 이전 이미지로 전환하는 메소드
    public void PreviousSprite()
    {
        currentSpriteIndex--;
        AudioPool.Instance.PlaySFX(0, 0.1f);
        if (currentSpriteIndex < startSpriteIndex)
        {
            currentSpriteIndex = endSpriteIndex;
        }
        UpdateSprite();
    }

    // Resources 폴더에서 스프라이트를 로드하여 SpriteRenderer에 적용
    private void UpdateSprite()
    {
        // 숫자가 두 자리 형식("01", "02")으로 표시되도록 포맷
        string spriteName = "Member_" + currentSpriteIndex.ToString("D2");
        Sprite newSprite = Resources.Load<Sprite>(spriteName);

        if (newSprite != null)
        {
            spriteRenderer.sprite = newSprite;
        }
        else
        {
            Debug.LogWarning("스프라이트를 찾을 수 없습니다: " + spriteName);
        }
    }
}
