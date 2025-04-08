using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkipBtn : MonoBehaviour
{


    public Button skipbutton;
    void Start()
    {
        skipbutton.interactable = true;
    }

    
    void Update()
    {
        if (Gamemanager.instance.clickcounter > 0 || Gamemanager.instance.cardcount == 0)
        {
            Gamemanager.instance.clickcounter -= Time.unscaledDeltaTime;
        }

        if (Gamemanager.instance.clickcounter > 10)
        {
            skipbutton.interactable = false;
        }
               
    }

    public void ClickCounter()
    {
        Gamemanager.instance.clickcounter++;
        Debug.Log($"{Gamemanager.instance.clickcounter}");
    }
        
}
