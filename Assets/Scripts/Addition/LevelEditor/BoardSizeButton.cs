using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSizeButton : MonoBehaviour
{
    public BoardSettingPanel panel;
    public SizeType sizeType;
    public int delta;
    // Start is called before the first frame update
    public void OnClick()
    {
        panel.TrySetBoardSize(sizeType, delta);
    }
}
