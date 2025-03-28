using UnityEngine;

[System.Serializable]
public class DialogueOption
{
    [TextArea(1, 3)] public string text;
    public string targetNodeID;
    public bool isExitOption;
    public int Affection;

    public bool EndsDay = false;
}