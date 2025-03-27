using UnityEngine;
using UnityEngine.SceneManagement; // Подключаем SceneManager

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Текущая сцена
        SceneManager.LoadScene(currentSceneIndex + 1); // Загружаем следующую
    }
    
    public void LoadNewNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Текущая сцена
        SceneManager.LoadScene(currentSceneIndex + 1); // Загружаем следующую
        DataManager.Instance.DeleteData();
    }



    public void QuitGame()
    {
        Debug.Log("Выход из игры!"); // Показывает сообщение в редакторе
        Application.Quit(); // Закрывает игру
    }

    
}
