using UnityEngine;

public class DataManager : MonoBehaviour
{
    // Singleton ��� ������� � ����������
    public static DataManager Instance;

    // ���������, ������� �� ����� ���������
    [SerializeField] int npcOldState;
    [SerializeField] int npcNewState;
    [SerializeField] int dayNumber;

    // Awake ���������� ��� �������� �������
    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            npcOldState = GameManager.Instance.npcOldState;
            npcNewState = GameManager.Instance.npcNewState;
            dayNumber = GameManager.Instance.dayNumber;
        }


        // ���������, ���������� �� ��� ���������. ���� ��� - ������� ���.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ��������� ������ ��� ����� ����
        }
        else
        {
            Destroy(gameObject); // ������� ��������, ���� ��������� ��� ����������
        }
    }

    // ����� ��� ���������� ������
    public void SaveData()
    {
        PlayerPrefs.SetInt("npcOldState", npcOldState);
        PlayerPrefs.SetInt("npcNewState", npcNewState);
        PlayerPrefs.SetInt("dayNumber", dayNumber);

        PlayerPrefs.Save();  // ��������� ���������
        Debug.Log("������ ���������!");
    }

    // ����� ��� �������� ������
    public void LoadData()
    {
        npcOldState = PlayerPrefs.GetInt("npcOldState", 0);  // 0 � ��������� ��������, ���� ����� ���
        npcNewState = PlayerPrefs.GetInt("npcNewState", 0);
        dayNumber = PlayerPrefs.GetInt("dayNumber", 0);

        Debug.Log("������ ���������! npcOldState: " + npcOldState + ", npcNewState: " + npcNewState + ", dayNumber: " + dayNumber);
    }

    // ����� ��� �������� ���� ������
    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();  // ��������� ���������
        Debug.Log("��� ���������� ������ �������!");
    }
}
