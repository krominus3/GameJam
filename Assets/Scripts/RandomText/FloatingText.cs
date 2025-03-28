using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class FloatingText : MonoBehaviour
{
    [Header("Phrases")]
    [SerializeField] private string[] Positive;
    [SerializeField] private string Neutral;
    [SerializeField] private string[] Negative;

    [Header("Settings")]
    public TextMeshProUGUI textPrefab;
    public Vector3 positionOffset = new Vector3(0, 1f, 0);
    public float fadeInDuration = 1f;
    public float displayDuration = 4f;
    public float fadeOutDuration = 1f;
    public bool isEventCharacter = false;

    [Header("References")]
    [SerializeField] private Canvas targetCanvas;
    private Camera mainCamera;
    private float lastSaid;

    private void OnEnable()
    {
        if(isEventCharacter)
            GameManager.Instance.PhraseCall.AddListener(SpawnFloatingText);
    }
    private void OnDisable()
    {
        if(isEventCharacter)
            GameManager.Instance.PhraseCall.RemoveListener(SpawnFloatingText);
    }

    private void Start()
    {
        mainCamera = Camera.main;
        targetCanvas = FindObjectOfType<Canvas>();
        
        if(targetCanvas == null)
        {
            Debug.LogError("No Canvas found in scene!");
            return;
        }
    }

    public void SpawnFloatingText()
    {
        if ((Time.time - lastSaid) < 4)
        {
            return;
        }
        

        TextMeshProUGUI textInstance = Instantiate(textPrefab, targetCanvas.transform);


        if (!isEventCharacter)
        {
            if(GameManager.Instance.npcNewState <= -2)
            {
                textInstance.text = Negative[Random.Range(0, Negative.Length)];
            }
            if(GameManager.Instance.npcNewState >= 2)
            {
                textInstance.text = Positive[Random.Range(0, Positive.Length)];
            }
            else
            {
                if (GameManager.Instance.dayNumber == 6)
                {
                    textInstance.text = Neutral;
                }
                else return;
            }
        }
        else
        {
            textInstance.text = Positive[Random.Range(0, Positive.Length)];

            if (Random.Range(0, 2) == 0)
            {
                return;
            }

        }
        

        CanvasGroup canvasGroup = textInstance.gameObject.AddComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        lastSaid = Time.time;
        UpdateTextPosition(textInstance);
        
        Sequence textSequence = DOTween.Sequence();
        
        textSequence.Append(canvasGroup.DOFade(1f, fadeInDuration));
        
        textSequence.AppendInterval(displayDuration);
        
        textSequence.Append(canvasGroup.DOFade(0f, fadeOutDuration));
        
        textSequence.OnComplete(() => 
        {
            Destroy(textInstance.gameObject);
        });

        textSequence.OnUpdate(() => UpdateTextPosition(textInstance));
    }

    void UpdateTextPosition(TextMeshProUGUI textInstance)
    {
        if(mainCamera && targetCanvas)
        {
            Vector3 worldPosition = transform.position + positionOffset;
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);
            textInstance.transform.position = screenPosition;
        }
    }

}