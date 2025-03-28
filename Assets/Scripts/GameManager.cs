using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public List<Sprite> emotions;
    public List<Color> colors;

    [SerializeField] private GameObject[] Characters;

    /*private struct Characters 
    {
        GameObject Character;
        Vector3 Position;
    }*/

    private GameObject Character;

    public int npcOldState = 0;
    public int npcNewState = 0;
    public int dayNumber = 0;

    public bool IsGoodNews;

    private float LastPhraseCall;

    public UnityEvent PhraseCall;

    
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        //��� ��������� �������. ����� ��� �������� ������ ����������, ����� ��������� �����������������.
        //DataManager.Instance.LoadData();
        
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (Character != null)
        {
            Destroy(Character);
        }
        if (npcOldState >= npcNewState)
            Character = Instantiate(Characters[Instance.dayNumber]);

        Debug.Log(Characters[Instance.dayNumber]);
    }



    /// <summary>
    /// � ����������� �� �������� ������ ��������� npcOldState � npcNewState. � ��� �� ��������� ������ � ������������� �������.
    /// </summary>
    /// <param name="goodNews">���� True, �� ���������� ����������, ���� false, �� ���������� ����������</param>
    public void EndDay()
    {
        dayNumber++;
        npcOldState = npcNewState;
        if (IsGoodNews)
            npcNewState++;
        else
            npcNewState--;

        DataManager.Instance.SaveData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    private void Update()
    {
        if (Time.time - LastPhraseCall <= 3)
        {
            LastPhraseCall = Time.time;
            PhraseCall.Invoke();
        }
    }

}
