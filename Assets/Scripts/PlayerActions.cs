using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    [SerializeField] float interactRange = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Use()
    {

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactRange);

        foreach (Collider2D hit in hits)
        {
            print(hit);
        }

        if (hits.Length > 1)
        {
            print("see object");
            print(hits[1]);

            if (hits[1].TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Use"))
        {
            Use();
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
}
}
