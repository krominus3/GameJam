using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public enum MoodType { Positive, Negative }
public enum ObjectType { NPC, Enemy, Neutral }

[System.Serializable]
public class PhraseData
{
    public ObjectType objectType;
    public MoodType mood;
    [TextArea(1, 3)] public List<string> phrases = new List<string>();
}

public class FloatingTextSpawner : MonoBehaviour
{
    [Header("Settings")]
    public float floatSpeed = 1f;
    public float lifeTime = 3f;
    public float fadeSpeed = 0.5f;
    public Color positiveColor = Color.green;
    public Color negativeColor = Color.red;

    [Header("References")]
    [SerializeField] private GameObject floatingTextPrefab;
    
    [Header("Phrases Database")]
    public List<PhraseData> phraseDatabase = new List<PhraseData>();

    private MoodType currentMood = MoodType.Neutral;

    public void SetMood(MoodType newMood)
    {
        currentMood = newMood;
    }

    public void SpawnFloatingText(ObjectType objectType)
    {
        List<string> availablePhrases = new List<string>();
        foreach(PhraseData data in phraseDatabase)
        {
            if(data.objectType == objectType && data.mood == currentMood)
            {
                availablePhrases.AddRange(data.phrases);
            }
        }

        if(availablePhrases.Count == 0)
        {
            Debug.LogWarning("No phrases found for this combination!");
            return;
        }


        string randomPhrase = availablePhrases[Random.Range(0, availablePhrases.Count)];
        
        GameObject textInstance = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
        TextMeshPro tmpComponent = textInstance.GetComponent<TextMeshPro>();
        
        tmpComponent.text = randomPhrase;
        tmpComponent.color = (currentMood == MoodType.Positive) ? positiveColor : negativeColor;
        
        StartCoroutine(FloatAndFade(textInstance.transform, tmpComponent));
    }

    private IEnumerator FloatAndFade(Transform textTransform, TextMeshPro tmp)
    {
        float timer = 0f;
        Vector3 startPosition = textTransform.position;

        while(timer < lifeTime)
        {
            textTransform.position = startPosition + Vector3.up * (floatSpeed * timer);
            
            if(timer > lifeTime - fadeSpeed)
            {
                float alpha = 1 - ((timer - (lifeTime - fadeSpeed)) / fadeSpeed);
                tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, alpha);
            }

            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(textTransform.gameObject);
    }
}