using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhotoMode : MonoBehaviour
{
    [SerializeField] float maxDistance = 5f;
    [SerializeField] float angle = 30f;
    [SerializeField] LayerMask targetLayer;
    


    private bool goodPhoto = false;
    private bool isPhotoMode = false;
    private Animator animator;
    private Transform playerTransform;
    private LineRenderer lineRenderer;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;

    private void Start()
    {
        LineRendererStart();
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerTransform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        animator.SetBool("isPhotoMode", isPhotoMode);

        if (isPhotoMode)
        {
            playerMovement.enabled = false;
            rb.velocity = Vector2.zero; // Останавливаем движение
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mousePos.x < transform.position.x)
                playerTransform.localScale = new Vector3(-1, 1, 1);
            else
                playerTransform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            playerMovement.enabled = true;
        }

        if (Input.GetButtonDown("Fire2") && !goodPhoto)
        {
            isPhotoMode = true;
            lineRenderer.enabled = true;
            rb.velocity = Vector2.zero; // Останавливаем движение
            print("Fire2 pressed - Photo Mode ON");
        }

        if (isPhotoMode)
        {
            DrawPhotoZone();
            if (Input.GetButtonDown("Fire1"))
            {
                print("Fire1 pressed - Taking Photo");
                TryMakePhoto();
            }
        }

        if (Input.GetButtonUp("Fire2"))
        {
            isPhotoMode = false;
            lineRenderer.enabled = false;
            print("Fire2 released - Photo Mode OFF");
        }
    }

    private void LineRendererStart()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 4;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.yellow;
        lineRenderer.enabled = false;
    }

    private void TryMakePhoto()
    {
        print("Make photo");
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        Vector2 direction = (mousePos - transform.position).normalized;
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, maxDistance, targetLayer);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Sensation"))
            {
                Vector2 toObject = (hit.transform.position - transform.position).normalized;
                float dot = Vector2.Dot(direction, toObject);
                float cosAngle = Mathf.Cos(angle * Mathf.Deg2Rad);
                if (dot > cosAngle)
                {
                    goodPhoto = true;
                    print("Good photo detected!");
                    return;
                }
            }
        }
        goodPhoto = false;
    }

    private void DrawPhotoZone()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        Vector2 direction = (mousePos - transform.position).normalized;
        Vector3 leftLimit = Quaternion.Euler(0, 0, angle) * direction * maxDistance;
        Vector3 rightLimit = Quaternion.Euler(0, 0, -angle) * direction * maxDistance;
        Vector3 start = transform.position;
        Vector3 leftPoint = start + leftLimit;
        Vector3 rightPoint = start + rightLimit;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, leftPoint);
        lineRenderer.SetPosition(2, rightPoint);
        lineRenderer.SetPosition(3, start);
    }
}
