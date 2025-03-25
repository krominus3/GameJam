using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class NpcBroadcastInfection : MonoBehaviour
{

    [SerializeField] float interactRange = 1.5f;
    [SerializeField] bool NewsPaper = false;

    [SerializeField] public int npcOldState;
    [SerializeField] public int npcNewState;

    [SerializeField] SpriteRenderer sr;
    private CircleCollider2D cc;

    // Start is called before the first frame update
    void Start()
    {
        npcOldState = GlobalVariables.Instance.npcOldState;
        npcNewState = NewsPaper ? GlobalVariables.Instance.npcNewState : npcOldState;

        cc = GetComponent<CircleCollider2D>();
        cc.radius = interactRange;
        //ChangeSprite();
    }


    void OnTriggerEnter2D(Collider2D hit)
    {
        if (!hit.CompareTag("NPC")) return;

        NpcBroadcastInfection otherNpc = hit.GetComponent<NpcBroadcastInfection>();
        if (otherNpc == null) return;

        // Если текущее состояние не изменилось, но у другого NPC есть новое состояние — принимаем его
        if (npcNewState == npcOldState && otherNpc.npcNewState != npcNewState)
        {
            Debug.Log($"Обновление состояния от {hit.gameObject.name}");
            npcNewState = otherNpc.npcNewState;
            ChangeSprite();
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
