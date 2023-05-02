using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCont: MonoBehaviour
{
    public Transform target; // ссылка на объект, за которым следит камера
    public float smoothSpeed = 0.125f; // скорость перемещения камеры

    private Vector3 offset; // расстояние между камерой и целью

    void Start()
    {
        offset = transform.position - target.position; // вычисляем расстояние между камерой и целью
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset; // вычисляем желаемую позицию камеры
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // сглаживаем перемещение камеры
        transform.position = smoothedPosition; // перемещаем камеру
    }
}

