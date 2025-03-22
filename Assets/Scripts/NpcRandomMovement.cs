using UnityEngine;

public class NpcRandomMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f; // Скорость движения
    [SerializeField] private float wanderRadius = 5f; // Радиус для случайной точки
    [SerializeField] private float stoppingDistance = 0.2f; // Насколько близко NPC должен подойти к точке
    [SerializeField] private LayerMask obstacleLayer; // Слой препятствий (стен)

    private Vector2 targetPosition;
    private bool isMoving = false;
    private bool facingRight = true; // Текущий поворот NPC

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
        int attempts = 10; // Количество попыток найти свободную точку
        for (int i = 0; i < attempts; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * wanderRadius;
            targetPosition = (Vector2)transform.position + randomOffset;

            // Проверяем, нет ли стены между текущей позицией и новой точкой
            if (!Physics2D.Linecast(transform.position, targetPosition, obstacleLayer))
            {
                isMoving = true;
                return;
            }
        }
    }

    void MoveToTarget()
    {
        // Проверяем, не уперся ли NPC в стену
        if (Physics2D.Linecast(transform.position, targetPosition, obstacleLayer))
        {
            isMoving = false; // Останавливаем движение
            return;
        }

        // Двигаемся к цели
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Флип персонажа при смене направления
        if ((targetPosition.x > transform.position.x && !facingRight) || (targetPosition.x < transform.position.x && facingRight))
        {
            Flip();
        }

        // Проверяем, дошел ли NPC до точки
        if (Vector2.Distance(transform.position, targetPosition) <= stoppingDistance)
        {
            isMoving = false;
        }
    }

    // Функция переворота по горизонтали
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // Визуализация в редакторе
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, wanderRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetPosition, 0.1f);
    }
}
