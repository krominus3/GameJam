using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcBroadcastInfection : MonoBehaviour
{

    [SerializeField] float interactRange = 2f;
    [SerializeField] bool NewsPaper = false;

    [SerializeField] public int npcOldState;
    [SerializeField] public int npcNewState;

    [SerializeField] SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        npcOldState = GlobalVariables.Instance.npcOldState;
        npcNewState = NewsPaper ? GlobalVariables.Instance.npcNewState : npcOldState;
        //ChangeSprite();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactRange);

        foreach (Collider2D hit in hits)
        {
            if (!hit.CompareTag("NPC")) continue;

            NpcBroadcastInfection otherNpc = hit.GetComponent<NpcBroadcastInfection>();
            if (otherNpc == null) continue;

            // Если текущее состояние не изменилось, но у другого NPC есть новое состояние — принимаем его
            if (npcNewState == npcOldState && otherNpc.npcNewState != npcNewState)
            {
                Debug.Log($"Обновление состояния от {hit.gameObject.name}");
                npcNewState = otherNpc.npcNewState;
                ChangeSprite();
            }
        }

    }

    void ChangeSprite()
    {
        sr.sprite = GlobalVariables.Instance.models[npcNewState];
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }

}
