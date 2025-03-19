using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target; // Ссылка на трансформ игрока 
    [SerializeField] float smoothSpeed = 0.125f; // Скорость сглаживания 
    [SerializeField] Vector3 offset; // Смещение камеры относительно игрока 

    void LateUpdate()
    {
        // Желаемая позиция камеры с учетом смещения 
        Vector3 desiredPosition = target.position + offset;

        // Интерполяция позиции камеры для плавного следования 
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Обновление позиции камеры 
        transform.position = smoothedPosition;
    }

    
}
