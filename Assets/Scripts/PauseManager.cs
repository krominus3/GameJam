using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu; // Ссылка на панель меню паузы
    private bool isPaused = false; // Флаг паузы

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Нажатие "Escape"
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
        pauseMenu.SetActive(true); // Показываем меню
        Time.timeScale = 0f; // Останавливаем время
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false); // Скрываем меню
        Time.timeScale = 1f; // Возвращаем время
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Восстанавливаем время перед загрузкой
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Текущая сцена
        SceneManager.LoadScene(currentSceneIndex - 1); // Загружаем главное меню
    }

    public void ReloadGame()
    {
        Time.timeScale = 1f; // Восстанавливаем время перед перезагрузкой
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
