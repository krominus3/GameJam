using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;
    float moveLimiter = 1f;
    float moveLimiterMin = 0.7f;
    float moveLimiterMax = 1f;

    public float runSpeed = 7.0f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
    }

    void FixedUpdate()
    {

        if (horizontal != 0)
        {
            transform.localScale = new Vector2(horizontal, 1f);
        }

        moveLimiter = (horizontal != 0 && vertical != 0) ? moveLimiterMin : moveLimiterMax;

        body.velocity = new Vector2(horizontal * runSpeed * moveLimiter, vertical * runSpeed * moveLimiter);

    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, 3f);
    //}
}
