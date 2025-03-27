using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public List<Sprite> emotions;
    public List<Color> colors;

    public int npcOldState = 0;
    public int npcNewState = 0;
    public int dayNumber = 0;

    
    private static GameManager instance;
    public static GameManager Instance

    {
        get { return instance; }
    }
    private void Awake()
    {
        //это временная строчка. нужна для удаления данных сохранения, чтобы проверять работоспособность.
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
    /// В зависимости от новостей меняет параметры npcOldState и npcNewState. А так же сохраняет данные и перезагружает уровень.
    /// </summary>
    /// <param name="goodNews">Если True, то настроение повышается, если false, то настроение понижается</param>
    public void EndDay(bool goodNews)
    {
        dayNumber++;
        npcOldState = npcNewState;
        if (goodNews)
            npcNewState++;
        else
            npcNewState--;

        DataManager.Instance.SaveData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
