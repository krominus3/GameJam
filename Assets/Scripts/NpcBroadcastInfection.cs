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

    [SerializeField] SpriteRenderer srHead;
    [SerializeField] SpriteRenderer srBody;
    [SerializeField] SpriteRenderer srEmotion;

    private CircleCollider2D cc;
    [SerializeField] private bool isMainHero;



    // Start is called before the first frame update
    void Start()
    {
        npcOldState = GameManager.Instance.npcOldState;
        npcNewState = NewsPaper ? GameManager.Instance.npcNewState : npcOldState;
        if (!NewsPaper)
        {
            ChangeSprite(npcOldState);
        }

        cc = GetComponent<CircleCollider2D>();
        cc.radius = interactRange;
    }


    void OnTriggerEnter2D(Collider2D hit)
    {
        if (!hit.CompareTag("NPC")) return;

        NpcBroadcastInfection otherNpc = hit.GetComponent<NpcBroadcastInfection>();
        if (otherNpc == null) return;

        if (otherNpc.gameObject.name != "gazeta" && otherNpc.GetComponent<FloatingText>().isEventCharacter == false && isMainHero == false)
            otherNpc.GetComponent<FloatingText>().SpawnFloatingText();

        // ���� ������� ��������� �� ����������, �� � ������� NPC ���� ����� ��������� � ��������� ���
        if (npcNewState == npcOldState && otherNpc.npcNewState != npcNewState)
        {
            Debug.Log($"���������� ��������� �� {hit.gameObject.name}");
            npcNewState = otherNpc.npcNewState;
            ChangeSprite(npcNewState);
        }
    }

    private int Abs(int x)
    {
        if (x < 0)
            return -x;
        return x;
    }

    private void ChangeSprite(int npcState)
    {
        srHead.color = GameManager.Instance.colors[Abs(npcState)];
        srBody.color = GameManager.Instance.colors[Abs(npcState)];

        int index = npcState switch
        {
            < -1 => 0,
            > 1 => 2,
            _ => 1
        };

        srEmotion.sprite = GameManager.Instance.emotions[index];

        //sr.sprite = GlobalVariables.Instance.models[npcNewState];
    }
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }

}
