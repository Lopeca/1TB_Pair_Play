using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditor : MonoBehaviour
{
    public static LevelEditor Instance { get; private set; }

    public Canvas canvas;
    public GameObject editorPanel;
    public RectTransform board;
    public GameObject cellPrefab;
    public GameObject cardPrefab;
    public int width = 1;
    public int height = 1;

    public float scale = 1;

    public Cell[,] cells;

    public LevelData levelData;

    public List<GameObject> cards;
    public int selectedCount;
    public Text selectCountText;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    internal void InitializeBySetting(int width, int height)
    {
        levelData = new LevelData();
        
        this.width = width;
        levelData.rows = width;
        this.height = height;
        levelData.cols = height;

        levelData.cardsPositions = new int[width * height];

        cells = new Cell[width, height];
        
        //보드에 셀 배치
        for (int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                GameObject cell = Instantiate(cellPrefab, board.gameObject.transform);
                RectTransform cellRect = cell.GetComponent<RectTransform>();
                             
                cellRect.sizeDelta = new Vector2(board.rect.width / width, board.rect.height / height);
                cellRect.anchoredPosition = new Vector2(j * cellRect.rect.width - board.rect.width/2, i * cellRect.rect.height - board.rect.height / 2);
                

                Cell cellComponent = cell.GetComponent<Cell>();
                cellComponent.id = i *width + j;
                cellComponent.posIndex = new Vector2(j, i);

                float offsetX = cellRect.rect.width / 2f;
                float offsetY = cellRect.rect.height / 2f;

                Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(null, cellRect.position + new Vector3(offsetX, offsetY));
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
              
                Debug.Log(worldPos);

                //GameObject card = Instantiate(cardPrefab);
                //card.transform.position = worldPos;

                cellComponent.worldPos = worldPos;
                cells[j,i] = cellComponent;
            }
        }

        editorPanel.SetActive(true);
    }

    public void Preview()
    {
        Debug.Log("카드 배율 : " + scale);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                RectTransform cellRect = cells[i, j].GetComponent<RectTransform>();
                float offsetX = cellRect.rect.width / 2f;
                float offsetY = cellRect.rect.height / 2f;
                Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(null, cellRect.position + new Vector3(offsetX, offsetY));
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

                Cell targetCell = cells[i, j];
                if (targetCell.isSelected)
                {
                    GameObject card = Instantiate(cardPrefab);
                    card.transform.position = worldPos + new Vector3(0, 0, 10);
                    
                    card.transform.localScale = Vector3.one * scale;

                    Debug.Log(card.transform.localScale);
                    cards.Add(card);
                }
                targetCell.gameObject.SetActive(false);

               
            }
        }
    }

    public void DisablePreview()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Cell targetCell = cells[i, j];
                targetCell.gameObject.SetActive(true);
            }
        }

        for (int i = 0; i < cards.Count; i++)
        {
            Destroy(cards[i]);
        }
        cards.Clear();
    }
    public void SaveLevel()
    {
        if(selectedCount % 2 != 0)
        {
            Debug.LogError("짝 맞아야 함");
        }
        string folder = Path.Combine(Application.streamingAssetsPath, "Levels");
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        string fileName = "NewLevel.json";
        string fullPath = Path.Combine(folder, fileName);

        levelData.cardScale = scale;
        levelData.cardsPositions = GenerateArrayFromCells();

        Debug.Log("저장 완료 StreamingAssets 폴더 확인");
        string json = JsonUtility.ToJson(levelData, true);
        File.WriteAllText(fullPath, json);
    }

    private int[] GenerateArrayFromCells()
    {
        int rows = cells.GetLength(0);
        int cols = cells.GetLength(1);

        // jagged array 생성 -> json화 가능
        int[] arr = new int[rows * cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                arr[i*cols + j] = cells[i, j].isSelected ? 1 : 0;
            }
        }

        return arr;
    }
}
