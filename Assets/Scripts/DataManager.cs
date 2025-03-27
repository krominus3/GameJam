using UnityEngine;

public class DataManager : MonoBehaviour
{
    // Singleton для доступа к экземпляру
    public static DataManager Instance;

    // Параметры, которые мы будем сохранять
    [SerializeField] int npcOldState;
    [SerializeField] int npcNewState;
    [SerializeField] int dayNumber;

    // Awake вызывается при создании объекта
    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            npcOldState = GameManager.Instance.npcOldState;
            npcNewState = GameManager.Instance.npcNewState;
            dayNumber = GameManager.Instance.dayNumber;
        }
        Debug.Log("Данные в игре! npcOldState: " + npcOldState + ", npcNewState: " + npcNewState + ", dayNumber: " + dayNumber);

        // Проверяем, существует ли уже экземпляр. Если нет - создаем его.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Сохраняем объект при смене сцен
        }
        else
        {
            Destroy(gameObject); // Удаляем дубликат, если экземпляр уже существует
        }
    }

    // Метод для сохранения данных
    public void SaveData()
    {
        PlayerPrefs.SetInt("npcOldState", GameManager.Instance.npcOldState);
        PlayerPrefs.SetInt("npcNewState", GameManager.Instance.npcNewState);
        PlayerPrefs.SetInt("dayNumber", GameManager.Instance.dayNumber);

        PlayerPrefs.Save();  // Сохраняем изменения
        Debug.Log("Данные сохранены! npcOldState: " + GameManager.Instance.npcOldState
                                + ", npcNewState: " + GameManager.Instance.npcNewState
                                + ", dayNumber: " + GameManager.Instance.dayNumber);
    }

    // Метод для загрузки данных
    public void LoadData()
    {
        GameManager.Instance.npcOldState = PlayerPrefs.GetInt("npcOldState", 0);  // 0 — дефолтное значение, если ключа нет
        GameManager.Instance.npcNewState = PlayerPrefs.GetInt("npcNewState", 0);
        GameManager.Instance.dayNumber = PlayerPrefs.GetInt("dayNumber", 0);

        Debug.Log("Данные загружены! npcOldState: " + GameManager.Instance.npcOldState
                                + ", npcNewState: " + GameManager.Instance.npcNewState
                                + ", dayNumber: " + GameManager.Instance.dayNumber);
    }

    // Метод для удаления всех данных
    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();  // Сохраняем изменения
        GameManager.Instance.npcOldState = 0;
        GameManager.Instance.npcNewState = 0;
        GameManager.Instance.dayNumber = 0;
        Debug.Log("Все сохранённые данные удалены!");
    }
}
