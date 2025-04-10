using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AudioPool : MonoBehaviour
{
    public static AudioPool Instance;
   // ����� ó�� �Ẹ�µ� ��� ��µǳ� �ñ��ؼ� ���̱��� �ẽ
    [Header("Audio Clips")]
    public AudioClip[] sfxClips;
    public AudioClip[] bgmClips;
    
    [Header("SFX Settings")]
    public int sfxPoolSize = 10;
    // ���� ��� ��� ���� ���� �Ǵµ� ��������� ������� �׳� �ھƳ���
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
    // ���� �̱��� �����
    void BuildAudioPool() // �� ���� �� �����Ǯ �����ϴ� �Լ�
    {
        // sfx �迭�� ������ҽ� �߰�
        for (int i = 0; i < sfxPoolSize; i++)
        {
            AudioSource sfx = gameObject.AddComponent<AudioSource>();
            sfx.loop = false;
            sfx.playOnAwake = false;
            sfx_AudioSources.Add(sfx);
            // 10ĭ�̴� ���Ŀ� ȿ���� �߰� ����
        }

        bgm_AudioSource = gameObject.AddComponent<AudioSource>();
        bgm_AudioSource.loop = true;
        bgm_AudioSource.playOnAwake=false;
    }

    // ���� ����ϱ�!
    public void PlaySFX(int sfx_index, float sfx_volume = 1f)
    {
        if (sfx_index < 0 || sfx_index >= sfxClips.Length || sfxClips[sfx_index] == null) return;
        // inspector���� �Ҵ����� ���� �ε����� ȣ���ϸ� ��� ���� = ��������
        AudioSource available = sfx_AudioSources.FirstOrDefault(a_source => !a_source.isPlaying);
        // �迭 sfx_AudioSources���� �÷��� ���� ������ҽ��� �ǳʶٰ�
        // ��� �ִ� ù ��° ������ҽ��� available��� �̸����� �Ҵ�
        if (available == null) return;
        else
        {
            available.clip = sfxClips[sfx_index];
            available.volume = sfx_volume;
            available.Play();
        }
    }
    // �Ʒ��� �ݺ��̶� �׳� ��. sfx ���� bgm����. bgm�� ���ÿ� �ϳ��� ����ϱ⿡ ������ҽ� 1��(bgm_AudioSource)�� �̿�.
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
