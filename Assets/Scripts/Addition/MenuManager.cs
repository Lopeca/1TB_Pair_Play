using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }
    public GameObject startPanel;

    public Stack<GameObject> menuStack;
    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        menuStack = new Stack<GameObject>();
        OpenMenu(startPanel);
    }

    public void OpenMenu(GameObject menu)
    {
        if(menuStack.Count > 0) menuStack.Peek().SetActive(false);
        menuStack.Push(menu);
        menu.SetActive(true);
    }

    public void CloseCurrentMenu()
    {
        menuStack.Pop().SetActive(false);
        menuStack.Peek().SetActive(true);
    }
}
