using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkipMatching : MonoBehaviour
{
    public Button skipbutton;
    public float clickcount = 0f;
        
    void Update()
    {       
        clickcount -= Time.deltaTime;
        clickcount = Mathf.Clamp(clickcount, 0f, 11f);
    }

    public void ClickCounter()
    {
        clickcount++;
        if (clickcount >= 10f) SceneManager.LoadScene("EndScene");
    }
}
