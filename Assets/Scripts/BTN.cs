using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BTN : MonoBehaviour
{
    // 멤버 카드 관련 변수
    public Button nextSpriteButton;      // 다음 이미지로 변경하는 버튼
    public Button previousSpriteButton;  // 이전 이미지로 변경하는 버튼
    public SpriteRenderer spriteRenderer; // 스프라이트 렌더러 (버튼 클릭 시 이미지가 변경될 대상)
    public int startSpriteIndex = 1;     // 스프라이트 파일의 시작 인덱스
    public int endSpriteIndex = 5;       // 스프라이트 파일의 종료 인덱스
    private int currentSpriteIndex;      // 현재 스프라이트 인덱스

    // 버튼 관련 및 씬 전환 관련 변수
    public AudioClip BtnSfx; // 버튼 클릭 사운드
    private AudioSource audioSource; // 오디오 소스

    private void Awake()
    {
        // 오디오 소스 초기화: 오디오 소스가 없으면 새로 생성
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false; // 오디오 소스가 자동으로 재생되지 않도록 설정
        audioSource.volume = 1.0f; // 볼륨 설정

        // 멤버 카드 기본 인덱스 초기화
        currentSpriteIndex = startSpriteIndex;
    }

    //---멤버 카드 메소드---//

    // 멤버 카드 이미지 업데이트 함수
    public void Start()
    {
        // 초기 이미지 설정
        UpdateSprite();

        // 버튼 이벤트 리스너 등록
        if (nextSpriteButton != null) // 다음 이미지 버튼이 설정되어 있으면
        {
            nextSpriteButton.onClick.AddListener(NextSprite); // 클릭 시 다음 이미지로 전환
        }
        if (previousSpriteButton != null) // 이전 이미지 버튼이 설정되어 있으면
        {
            previousSpriteButton.onClick.AddListener(PreviousSprite); // 클릭 시 이전 이미지로 전환
        }
    }

    // 멤버카드 이미지 업데이트 함수
    private void UpdateSprite()
    {
        // 숫자를 두 자리 형식("01", "02")으로 포맷하여 Resources 폴더에서 스프라이트를 로드
        string SpriteName = "Member_" + currentSpriteIndex.ToString("D2");
        Sprite newSprite = Resources.Load<Sprite>(SpriteName);

        if (newSprite != null)
        {
            spriteRenderer.sprite = newSprite; // 스프라이트 렌더러에 새 스프라이트 적용
        }
        else
        {
            Debug.Log("스프라이트를 찾을 수 없습니다: " + SpriteName); // 스프라이트 로드 실패 시 에러 메시지 출력
        }
    }

    // 다음 이미지로 전환하는 메소드
    public void NextSprite()
    {
        // 버튼 클릭 효과음 재생
        if (audioSource != null && BtnSfx != null)
        {
            audioSource.PlayOneShot(BtnSfx);
        }

        currentSpriteIndex++; // 현재 스프라이트 인덱스 증가
        if (currentSpriteIndex > endSpriteIndex) // 인덱스가 종료 인덱스를 초과하면
        {
            currentSpriteIndex = startSpriteIndex; // 시작 인덱스로 초기화
        }
        UpdateSprite(); // 스프라이트 업데이트
    }

    // 이전 이미지로 전환하는 메소드
    public void PreviousSprite()
    {
        // 버튼 클릭 효과음 재생
        if (audioSource != null && BtnSfx != null)
        {
            audioSource.PlayOneShot(BtnSfx);
        }
        currentSpriteIndex--; // 현재 스프라이트 인덱스 감소
        if (currentSpriteIndex < startSpriteIndex) // 인덱스가 시작 인덱스를 미만이면
        {
            currentSpriteIndex = endSpriteIndex; // 종료 인덱스로 초기화
        }
        UpdateSprite(); // 스프라이트 업데이트
    }

    //---버튼 클릭 사운드 및 씬 전환 메소드---//

    // 게임 시작 버튼
    public void StartUI()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(BtnSfx);
        }
        Invoke("MainScene", 0.5f);
    }
    // 메인 씬으로 전환
    public void MainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    // 타이틀로 돌아가는 버튼
    public void TitleUI()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(BtnSfx);
        }
        Invoke("TitleScene", 0.5f);
    }
    // 타이틀 씬으로 전환
    public void TitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
