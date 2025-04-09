using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemberCard : MonoBehaviour
{
    // UI Button ������Ʈ (Inspector���� �Ҵ�)
    public Button nextSpriteButton;      // ���� �̹����� �����ϴ� ��ư
    public Button previousSpriteButton;  // ���� �̹����� �����ϴ� ��ư

    // ��������Ʈ ������ (��ư Ŭ�� �� �̹����� ����� ���)
    public SpriteRenderer spriteRenderer;

    // ��������Ʈ ������ ���� �� ���� �ε���
    public int startSpriteIndex = 1;
    public int endSpriteIndex = 5;

    private int currentSpriteIndex;

    // Start is called before the first frame update
    void Start()
    {
        // �ʱ� �̹��� �ε��� ����
        currentSpriteIndex = startSpriteIndex;
        UpdateSprite();

        // ��ư �̺�Ʈ ������ ���
        if (nextSpriteButton != null)
        {
            nextSpriteButton.onClick.AddListener(NextSprite);
        }
        if (previousSpriteButton != null)
        {
            previousSpriteButton.onClick.AddListener(PreviousSprite);
        }
    }
    // ���� �̹����� ��ȯ�ϴ� �޼ҵ�
    public void NextSprite()
    {
        Debug.Log("NextSprite() ȣ���. ���� �ε���: " + currentSpriteIndex);
        currentSpriteIndex++; // ���� �ε��� ����
        if (currentSpriteIndex > endSpriteIndex) // ���� �ε����� ���� �ε����� �ʰ��ϸ�
        {
            currentSpriteIndex = startSpriteIndex; // ���� �ε����� ���ư�
        }
        UpdateSprite(); // ��������Ʈ ������Ʈ
    }

    // ���� �̹����� ��ȯ�ϴ� �޼ҵ�
    public void PreviousSprite()
    {
        currentSpriteIndex--; // ���� �ε��� ����
        if (currentSpriteIndex < startSpriteIndex) // ���� �ε����� ���� �ε����� �̸��̸�
        {
            currentSpriteIndex = endSpriteIndex; // ���� �ε����� ���ư�
        }
        UpdateSprite(); // ��������Ʈ ������Ʈ
    }

    // Resources �������� ��������Ʈ�� �ε��Ͽ� SpriteRenderer�� ����
    private void UpdateSprite()
    {
        // ���ڰ� �� �ڸ� ����("01", "02")���� ǥ�õǵ��� ����
        string spriteName = "Member_" + currentSpriteIndex.ToString("D2");
        Sprite newSprite = Resources.Load<Sprite>(spriteName); // ��������Ʈ �ε�

        if (newSprite != null) // ��������Ʈ�� �����ϸ�
        {
            spriteRenderer.sprite = newSprite; // ��������Ʈ �������� �� ��������Ʈ ����
        }
        else
        {
            Debug.LogWarning("��������Ʈ�� ã�� �� �����ϴ�: " + spriteName);
        }
    }
}
