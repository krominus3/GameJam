using UnityEngine;
using UnityEngine.SceneManagement; // Подключаем SceneManager

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Текущая сцена
        SceneManager.LoadScene(currentSceneIndex + 1); // Загружаем следующую
    }
}
