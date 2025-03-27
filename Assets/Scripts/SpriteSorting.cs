using UnityEngine;

public class SpriteSorting : MonoBehaviour
{
    private SpriteRenderer[] spriteRenderers;
    private int[] initialSortingOrders; // Хранит изначальный order каждого спрайта

    void Start()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>(); // Берём все спрайты
        initialSortingOrders = new int[spriteRenderers.Length];

        // Запоминаем их начальный sortingOrder
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            initialSortingOrders[i] = spriteRenderers[i].sortingOrder;
        }
    }

    void Update()
    {
        int baseSortingOrder = Mathf.RoundToInt(transform.position.y * -10); // Чем ниже Y, тем выше слой

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].sortingOrder = baseSortingOrder + initialSortingOrders[i]; // Учитываем изначальный порядок
        }
    }
}
