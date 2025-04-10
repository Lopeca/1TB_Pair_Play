using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AudioPool : MonoBehaviour
{
    public static AudioPool Instance;
   // 헤더는 처음 써보는데 어떻게 출력되나 궁금해서 굳이굳이 써봄
    [Header("Audio Clips")]
    public AudioClip[] sfxClips;
    public AudioClip[] bgmClips;
    
    [Header("SFX Settings")]
    public int sfxPoolSize = 10;
    // 위의 어레이 요소 수를 세도 되는데 오류날까봐 무서우니 그냥 박아놓음
    private List<AudioSource> sfx_AudioSources = new List<AudioSource>();

    [Header("BGM Settings")]
    private AudioSource bgm_AudioSource;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            BuildAudioPool();
        }
        else Destroy(gameObject);
    }
    // 흔한 싱글톤 만들기
    void BuildAudioPool() // 겜 시작 시 오디오풀 생성하는 함수
    {
        // sfx 배열에 오디오소스 추가
        for (int i = 0; i < sfxPoolSize; i++)
        {
            AudioSource sfx = gameObject.AddComponent<AudioSource>();
            sfx.loop = false;
            sfx.playOnAwake = false;
            sfx_AudioSources.Add(sfx);
            // 10칸이니 추후에 효과음 추가 가능
        }

        bgm_AudioSource = gameObject.AddComponent<AudioSource>();
        bgm_AudioSource.loop = true;
        bgm_AudioSource.playOnAwake=false;
    }

    // 이하 재생하기!
    public void PlaySFX(int sfx_index, float sfx_volume = 1f)
    {
        if (sfx_index < 0 || sfx_index >= sfxClips.Length || sfxClips[sfx_index] == null) return;
        // inspector에서 할당하지 않은 인덱스를 호출하면 즉시 종료 = 에러방지
        AudioSource available = sfx_AudioSources.FirstOrDefault(a_source => !a_source.isPlaying);
        // 배열 sfx_AudioSources에서 플레이 중인 오디오소스는 건너뛰고
        // 놀고 있는 첫 번째 오디오소스를 available라는 이름으로 할당
        if (available == null) return;
        else
        {
            available.clip = sfxClips[sfx_index];
            available.volume = sfx_volume;
            available.Play();
        }
    }
    // 아래는 반복이라 그냥 둠. sfx 말고 bgm으로. bgm은 동시에 하나만 재생하기에 오디오소스 1개(bgm_AudioSource)만 이용.
    public void PlayBGM(int bgm_index, float bgm_volume = 1f)
    {
        if (bgm_index < 0 || bgm_index >= bgmClips.Length || bgmClips[bgm_index] == null) return;

        if (bgm_AudioSource.clip != bgmClips[bgm_index])
        {
            StopBGM();
            bgm_AudioSource.clip = bgmClips[bgm_index];
            bgm_AudioSource.volume = bgm_volume;
            bgm_AudioSource.Play();
        }
        Debug.Log(bgm_index);
        Debug.Log(bgm_volume);
    }

    public void StopBGM()
    {
        bgm_AudioSource.Stop();
    }

}
