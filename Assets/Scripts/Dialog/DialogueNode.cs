using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

[System.Serializable]
public class DialogueNode
{
    [Header("Meta")]
    public string id;
    
    [Header("Content")]
    public string characterName;
    [TextArea(3, 5)] public string text;
    
    [Header("Options")]
    public List<DialogueOption> options = new List<DialogueOption>();
    
}