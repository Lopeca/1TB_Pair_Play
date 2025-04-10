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

        //폴더 내 파일 갯수 구하기
        string[] files = Directory.GetFiles(folderPath, "level*.json");

        //위 상태로 방치하면 level2.json보다 level10.json이 먼저 옴
        var sortedFiles = files.Select(path => new
        {
            Path = path,
            Index = ExtractLevelIndex(Path.GetFileNameWithoutExtension(path)) // level10 → 10
        })
            .OrderBy(x => x.Index)
            .ToList();


        //파일명 level0.json, level1.json 이렇게 막 있음. 파일 갯수 구해서 그만큼 인스턴스화시키기
        for (int i = 0; i < files.Length; i++)
        {
            GameObject stageButton = Instantiate(stageButtonPrefab, contentBox.transform);
            stageButton.GetComponent<StageButton>().SetIndex(i);
        }

    }

    private object ExtractLevelIndex(string fileName)
    {
        // "level12" 에서 숫자만 추출 (정규식 사용)
        Match match = Regex.Match(fileName, @"\d+");
        return match.Success ? int.Parse(match.Value) : -1;
    }
}
