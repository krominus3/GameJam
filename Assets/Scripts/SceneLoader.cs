using UnityEngine;
using UnityEngine.SceneManagement; // ���������� SceneManager

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // ������� �����
        SceneManager.LoadScene(currentSceneIndex + 1); // ��������� ���������
    }
    
    public void LoadNewNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // ������� �����
        SceneManager.LoadScene(currentSceneIndex + 1); // ��������� ���������
        DataManager.Instance.DeleteData();
    }



    public void QuitGame()
    {
        Debug.Log("����� �� ����!"); // ���������� ��������� � ���������
        Application.Quit(); // ��������� ����
    }

    
}
