using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class LevelSelectPanel : MonoBehaviour
{
    public GameObject contentBox;
    public GameObject stageButtonPrefab;
    public void OnClickGoBackButton()
    {
        MenuManager.Instance.CloseCurrentMenu();
    }

    public void OnEnable()
    {
        SpawnStageButtons();
    }

    private void SpawnStageButtons()
    {
        string folderPath = Path.Combine(Application.streamingAssetsPath, "Levels");

        //���� �� ���� ���� ���ϱ�
        string[] files = Directory.GetFiles(folderPath, "level*.json");

        //�� ���·� ��ġ�ϸ� level2.json���� level10.json�� ���� ��
        var sortedFiles = files.Select(path => new
        {
            Path = path,
            Index = ExtractLevelIndex(Path.GetFileNameWithoutExtension(path)) // level10 �� 10
        })
            .OrderBy(x => x.Index)
            .ToList();


        //���ϸ� level0.json, level1.json �̷��� �� ����. ���� ���� ���ؼ� �׸�ŭ �ν��Ͻ�ȭ��Ű��
        for (int i = 0; i < files.Length; i++)
        {
            GameObject stageButton = Instantiate(stageButtonPrefab, contentBox.transform);
            stageButton.GetComponent<StageButton>().SetIndex(i);
        }

    }

    private object ExtractLevelIndex(string fileName)
    {
        // "level12" ���� ���ڸ� ���� (���Խ� ���)
        Match match = Regex.Match(fileName, @"\d+");
        return match.Success ? int.Parse(match.Value) : -1;
    }
}
