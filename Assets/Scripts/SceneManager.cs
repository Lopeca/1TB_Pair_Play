using UnityEngine;
using UnityEngine.SceneManagement;

public class Retrybtn : MonoBehaviour
{
    public void LoadScene()
    {
        Invoke("Retry", 0.5f);
    }
    // ���� �� �߰� �� ���⼭ ������ ��!
    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void StartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
