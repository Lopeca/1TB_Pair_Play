using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    public Text stageText;
    public int index;

    public void SetIndex(int num)
    {
        index = num;
        stageText.text = "Stage " + index.ToString();
    }

    public void OnClickStageButton()
    {
        LevelDataCarrier.Instance.levelNum = index;
        SceneManager.LoadScene("MainScene");
    }
}
