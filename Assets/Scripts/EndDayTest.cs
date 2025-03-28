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
        //�������� ���� ����� � ������
        if (isGoodPhoto)
            GameManager.Instance.EndDay();
            //�� ������� �������� true ��� ��������. ������� �� ������� ��� ��������� ������� ����������.
    }

}
