using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndDayTest : MonoBehaviour, IInteractable
{

    [SerializeField] PlayerPhotoMode photoMode;
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
        
        bool isGoodPhoto = photoMode.goodPhoto;
        print(isGoodPhoto);
        //проверка если фотка у игрока
        if (isGoodPhoto)
            GameManager.Instance.EndDay(true);
            //по дефолту поставил true для проверки. Перейди по функции для просмотра большей информации.
    }

}
