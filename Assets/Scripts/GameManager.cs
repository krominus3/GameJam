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
