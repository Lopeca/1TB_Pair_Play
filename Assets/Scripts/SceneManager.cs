using UnityEngine;
using UnityEngine.SceneManagement;

public class Retrybtn : MonoBehaviour
{
    // ���� �� �߰� �� ���⼭ ������ ��!
    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }
}
