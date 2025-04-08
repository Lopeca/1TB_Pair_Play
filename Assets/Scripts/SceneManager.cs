using UnityEngine;
using UnityEngine.SceneManagement;

public class Retrybtn : MonoBehaviour
{
    // 추후 신 추가 시 여기서 관리할 것!
    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }
}
