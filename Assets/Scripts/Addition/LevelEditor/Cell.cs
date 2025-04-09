using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public int id;
    public bool isSelected;
    public Vector2 posIndex;
    public Vector2 worldPos;

    public Image image;

    public void OnClickCard()
    {
        isSelected = !isSelected;
        image.color = isSelected? Color.red : Color.white;
        LevelEditor.Instance.selectedCount = isSelected ? LevelEditor.Instance.selectedCount + 1 : LevelEditor.Instance.selectedCount - 1;
        LevelEditor.Instance.selectCountText.text = LevelEditor.Instance.selectedCount.ToString();
    }
}
