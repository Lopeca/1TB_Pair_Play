using UnityEngine;
using UnityEngine.SceneManagement;

public class Retrybtn : MonoBehaviour
{
    public void LoadScene()
    {
        Invoke("Retry", 0.5f);
    }
    // 추후 신 추가 시 여기서 관리할 것!
    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void StartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
