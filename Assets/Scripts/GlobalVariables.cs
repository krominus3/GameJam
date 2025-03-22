using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalVariables : MonoBehaviour
{
    // This is where you create the global variable that can be accessed from anywhere.

    public List<Sprite> models;

    public int npcOldState = 0;
    public int npcNewState = 1;

    // This part helps ensure there's only one instance of this class.
    private static GlobalVariables instance;
    public static GlobalVariables Instance

    {
        get { return instance; }
    }
    private void Awake()
    {
        // Check if there's already an instance of this class. If yes, destroy this one. If not, set this as the instance.
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
}
