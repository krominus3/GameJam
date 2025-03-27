using UnityEngine;
using UnityEngine.SceneManagement; // ���������� SceneManager

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // ������� �����
        SceneManager.LoadScene(currentSceneIndex + 1); // ��������� ���������
    }
}
