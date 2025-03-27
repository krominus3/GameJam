using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndDayTest : MonoBehaviour, IInteractable
{
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public void Interact()
    {
        GameManager.Instance.EndDay(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
