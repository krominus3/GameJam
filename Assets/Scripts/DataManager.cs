using UnityEngine;

public class DataManager : MonoBehaviour
{
    // Singleton ��� ������� � ����������
    public static DataManager Instance;

    // ���������, ������� �� ����� ���������
    [SerializeField] int npcOldState;
    [SerializeField] int npcNewState;
    [SerializeField] int dayNumber;
    [SerializeField] private int diagNumero;

    // Awake ���������� ��� �������� �������
    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            npcOldState = GameManager.Instance.npcOldState;
            npcNewState = GameManager.Instance.npcNewState;
            dayNumber = GameManager.Instance.dayNumber;
        }
        if (DiagManager.Instance != null)
        { 
            diagNumero = DiagManager.Instance.DialogueNumero;
        }
        Debug.Log("������ � ����! npcOldState: " + npcOldState + ", npcNewState: " + npcNewState + ", dayNumber: " + dayNumber);

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
        PlayerPrefs.SetInt("npcOldState", GameManager.Instance.npcOldState);
        PlayerPrefs.SetInt("npcNewState", GameManager.Instance.npcNewState);
        PlayerPrefs.SetInt("dayNumber", GameManager.Instance.dayNumber);
        PlayerPrefs.SetInt("diagNumero", DiagManager.Instance.DialogueNumero);

        PlayerPrefs.Save();  // ��������� ���������
        Debug.Log("������ ���������! npcOldState: " + GameManager.Instance.npcOldState
                                + ", npcNewState: " + GameManager.Instance.npcNewState
                                + ", dayNumber: " + GameManager.Instance.dayNumber
                                + ", diagNumero" + DiagManager.Instance.DialogueNumero);
    }

    // ����� ��� �������� ������
    public void LoadData()
    {
        GameManager.Instance.npcOldState = PlayerPrefs.GetInt("npcOldState", 0);  // 0 � ��������� ��������, ���� ����� ���
        GameManager.Instance.npcNewState = PlayerPrefs.GetInt("npcNewState", 0);
        GameManager.Instance.dayNumber = PlayerPrefs.GetInt("dayNumber", 0);
        DiagManager.Instance.DialogueNumero = PlayerPrefs.GetInt("diagNumero", 0);

        Debug.Log("������ ���������! npcOldState: " + GameManager.Instance.npcOldState
                                + ", npcNewState: " + GameManager.Instance.npcNewState
                                + ", dayNumber: " + GameManager.Instance.dayNumber
                                + ", diagNumero" + DiagManager.Instance.DialogueNumero);
    }

    // ����� ��� �������� ���� ������
    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();  // ��������� ���������
        GameManager.Instance.npcOldState = 0;
        GameManager.Instance.npcNewState = 0;
        GameManager.Instance.dayNumber = 0;
        Debug.Log("��� ���������� ������ �������!");
    }
}
