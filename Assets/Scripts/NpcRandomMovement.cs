using UnityEngine;

public class NpcRandomMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f; // �������� ��������
    [SerializeField] private float wanderRadius = 5f; // ������ ��� ��������� �����
    [SerializeField] private float stoppingDistance = 0.2f; // ��������� ������ NPC ������ ������� � �����
    [SerializeField] private LayerMask obstacleLayer; // ���� ����������� (����)

    private Vector2 targetPosition;
    private bool isMoving = false;
    private bool facingRight = true; // ������� ������� NPC

    void Start()
    {
        SetNewTarget();
    }

    void Update()
    {
        if (isMoving)
        {
            MoveToTarget();
        }
        else
        {
            SetNewTarget();
        }
    }

    void SetNewTarget()
    {
        int attempts = 10; // ���������� ������� ����� ��������� �����
        for (int i = 0; i < attempts; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * wanderRadius;
            targetPosition = (Vector2)transform.position + randomOffset;

            // ���������, ��� �� ����� ����� ������� �������� � ����� ������
            if (!Physics2D.Linecast(transform.position, targetPosition, obstacleLayer))
            {
                isMoving = true;
                return;
            }
        }
    }

    void MoveToTarget()
    {
        // ���������, �� ������ �� NPC � �����
        if (Physics2D.Linecast(transform.position, targetPosition, obstacleLayer))
        {
            isMoving = false; // ������������� ��������
            return;
        }

        // ��������� � ����
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // ���� ��������� ��� ����� �����������
        if ((targetPosition.x > transform.position.x && !facingRight) || (targetPosition.x < transform.position.x && facingRight))
        {
            Flip();
        }

        // ���������, ����� �� NPC �� �����
        if (Vector2.Distance(transform.position, targetPosition) <= stoppingDistance)
        {
            isMoving = false;
        }
    }

    // ������� ���������� �� �����������
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // ������������ � ���������
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, wanderRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetPosition, 0.1f);
    }
}
