using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NPCDialogueTrigger.cs
public class NPCDialogueTrigger : MonoBehaviour
{
    public DialogueData dialogueData;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.E) && other.CompareTag("Player"))
        {
            DiagManager.Instance.StartDialogue(dialogueData);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DiagManager.Instance.EndDialogue();
        }
    }
}
