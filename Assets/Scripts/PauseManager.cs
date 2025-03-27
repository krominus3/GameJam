using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu; // ������ �� ������ ���� �����
    private bool isPaused = false; // ���� �����

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // ������� "Escape"
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true); // ���������� ����
        Time.timeScale = 0f; // ������������� �����
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false); // �������� ����
        Time.timeScale = 1f; // ���������� �����
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // ��������������� ����� ����� ���������
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // ������� �����
        SceneManager.LoadScene(currentSceneIndex - 1); // ��������� ������� ����
    }

    public void ReloadGame()
    {
        Time.timeScale = 1f; // ��������������� ����� ����� �������������
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
