using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BTN : MonoBehaviour
{
    // ��� ī�� ���� ����
    public Button nextSpriteButton;      // ���� �̹����� �����ϴ� ��ư
    public Button previousSpriteButton;  // ���� �̹����� �����ϴ� ��ư
    public SpriteRenderer spriteRenderer; // ��������Ʈ ������ (��ư Ŭ�� �� �̹����� ����� ���)
    public int startSpriteIndex = 1;     // ��������Ʈ ������ ���� �ε���
    public int endSpriteIndex = 5;       // ��������Ʈ ������ ���� �ε���
    private int currentSpriteIndex;      // ���� ��������Ʈ �ε���

    // ��ư ���� �� �� ��ȯ ���� ����
    public AudioClip BtnSfx; // ��ư Ŭ�� ����
    private AudioSource audioSource; // ����� �ҽ�

    private void Awake()
    {
        // ����� �ҽ� �ʱ�ȭ: ����� �ҽ��� ������ ���� ����
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false; // ����� �ҽ��� �ڵ����� ������� �ʵ��� ����
        audioSource.volume = 1.0f; // ���� ����

        // ��� ī�� �⺻ �ε��� �ʱ�ȭ
        currentSpriteIndex = startSpriteIndex;
    }

    //---��� ī�� �޼ҵ�---//

    // ��� ī�� �̹��� ������Ʈ �Լ�
    public void Start()
    {
        // �ʱ� �̹��� ����
        UpdateSprite();

        // ��ư �̺�Ʈ ������ ���
        if (nextSpriteButton != null) // ���� �̹��� ��ư�� �����Ǿ� ������
        {
            nextSpriteButton.onClick.AddListener(NextSprite); // Ŭ�� �� ���� �̹����� ��ȯ
        }
        if (previousSpriteButton != null) // ���� �̹��� ��ư�� �����Ǿ� ������
        {
            previousSpriteButton.onClick.AddListener(PreviousSprite); // Ŭ�� �� ���� �̹����� ��ȯ
        }
    }

    // ���ī�� �̹��� ������Ʈ �Լ�
    private void UpdateSprite()
    {
        // ���ڸ� �� �ڸ� ����("01", "02")���� �����Ͽ� Resources �������� ��������Ʈ�� �ε�
        string SpriteName = "Member_" + currentSpriteIndex.ToString("D2");
        Sprite newSprite = Resources.Load<Sprite>(SpriteName);

        if (newSprite != null)
        {
            spriteRenderer.sprite = newSprite; // ��������Ʈ �������� �� ��������Ʈ ����
        }
        else
        {
            Debug.Log("��������Ʈ�� ã�� �� �����ϴ�: " + SpriteName); // ��������Ʈ �ε� ���� �� ���� �޽��� ���
        }
    }

    // ���� �̹����� ��ȯ�ϴ� �޼ҵ�
    public void NextSprite()
    {
        // ��ư Ŭ�� ȿ���� ���
        if (audioSource != null && BtnSfx != null)
        {
            audioSource.PlayOneShot(BtnSfx);
        }

        currentSpriteIndex++; // ���� ��������Ʈ �ε��� ����
        if (currentSpriteIndex > endSpriteIndex) // �ε����� ���� �ε����� �ʰ��ϸ�
        {
            currentSpriteIndex = startSpriteIndex; // ���� �ε����� �ʱ�ȭ
        }
        UpdateSprite(); // ��������Ʈ ������Ʈ
    }

    // ���� �̹����� ��ȯ�ϴ� �޼ҵ�
    public void PreviousSprite()
    {
        // ��ư Ŭ�� ȿ���� ���
        if (audioSource != null && BtnSfx != null)
        {
            audioSource.PlayOneShot(BtnSfx);
        }
        currentSpriteIndex--; // ���� ��������Ʈ �ε��� ����
        if (currentSpriteIndex < startSpriteIndex) // �ε����� ���� �ε����� �̸��̸�
        {
            currentSpriteIndex = endSpriteIndex; // ���� �ε����� �ʱ�ȭ
        }
        UpdateSprite(); // ��������Ʈ ������Ʈ
    }

    //---��ư Ŭ�� ���� �� �� ��ȯ �޼ҵ�---//

    // ���� ���� ��ư
    public void StartUI()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(BtnSfx);
        }
        Invoke("MainScene", 0.5f);
    }
    // ���� ������ ��ȯ
    public void MainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    // Ÿ��Ʋ�� ���ư��� ��ư
    public void TitleUI()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(BtnSfx);
        }
        Invoke("TitleScene", 0.5f);
    }
    // Ÿ��Ʋ ������ ��ȯ
    public void TitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
