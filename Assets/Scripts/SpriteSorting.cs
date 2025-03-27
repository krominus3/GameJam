using UnityEngine;

public class SpriteSorting : MonoBehaviour
{
    private SpriteRenderer[] spriteRenderers;
    private int[] initialSortingOrders; // ������ ����������� order ������� �������

    void Start()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>(); // ���� ��� �������
        initialSortingOrders = new int[spriteRenderers.Length];

        // ���������� �� ��������� sortingOrder
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            initialSortingOrders[i] = spriteRenderers[i].sortingOrder;
        }
    }

    void Update()
    {
        int baseSortingOrder = Mathf.RoundToInt(transform.position.y * -10); // ��� ���� Y, ��� ���� ����

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].sortingOrder = baseSortingOrder + initialSortingOrders[i]; // ��������� ����������� �������
        }
    }
}
