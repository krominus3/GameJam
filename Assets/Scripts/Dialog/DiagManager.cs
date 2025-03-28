using UnityEngine;
using TMPro; // Добавьте это пространство имён
using UnityEngine.UI;
using System.Collections.Generic;

public class DiagManager : MonoBehaviour
{
    public static DiagManager Instance { get; private set; }

    [Header("Diag Params")]
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private TextMeshProUGUI characterNameText; 
    [SerializeField] private TextMeshProUGUI dialogueText;    
    [SerializeField] private Transform optionsContainer;
    [SerializeField] private GameObject optionButtonPrefab; 
    [SerializeField] private NPCDialogueTrigger npcDiagComponent;


    [Header("Diag Data")]
    [SerializeField] private List<DialogueData> Dialgs;
    [SerializeField] private int _dialogueNumero = 0;



    public int DialogueNumero
    {
        get
        {
            return _dialogueNumero;
        }
        set
        {
            _dialogueNumero = value;
            npcDiagComponent.dialogueData = Dialgs[value];
        }
    }

    private DialogueData _currentDialogue;
    private DialogueNode _currentNode;

    private void Awake()
    {
        DialogueNumero = GameManager.Instance.DialogueNumero;
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        npcDiagComponent.dialogueData = Dialgs[DialogueNumero];
    }

    public void SelectDialogueByID(int id)
    {
        DialogueNumero = id;
    }

    public void NextDiag()
    {
        GameManager.Instance.DialogueNumero += 1;
        DialogueNumero += 1;
    }

    public void StartDialogue(DialogueData dialogue)
    {
        _currentDialogue = dialogue;
        _currentNode = FindNode(dialogue.startNodeID);
        dialogueUI.SetActive(true);
        UpdateDialogueUI();
    }
    

    private void UpdateDialogueUI()
    {
        characterNameText.text = _currentNode.characterName;
        dialogueText.text = _currentNode.text;
        ClearOptions();

        foreach (var option in _currentNode.options)
        {
            GameObject button = Instantiate(optionButtonPrefab, optionsContainer);
            button.GetComponentInChildren<TextMeshProUGUI>().text = option.text;
            button.GetComponent<Button>().onClick.AddListener(() => SelectOption(option));
        }

    }

    private void SelectOption(DialogueOption option)
    {
        if (option.Affection == -1)
            GameManager.Instance.IsGoodNews = false;
        if (option.Affection == 1)
            GameManager.Instance.IsGoodNews = true;
        
        if (option.EndsDay == true)
        {
            EndDialogue();
            GameManager.Instance.EndDay();
            return;
        }

        if (option.isExitOption)
        {
            EndDialogue();
            return;
        }

        _currentNode = FindNode(option.targetNodeID);
        UpdateDialogueUI();
    }

    private DialogueNode FindNode(string id)
    {
        return _currentDialogue.nodes.Find(node => node.id == id);
    }

    private void ClearOptions()
    {
        foreach (Transform child in optionsContainer) Destroy(child.gameObject);
    }

    public void EndDialogue()
    {
        dialogueUI.SetActive(false);
    }
}

