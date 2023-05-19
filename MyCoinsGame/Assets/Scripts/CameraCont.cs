/*using System.Collections;
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

*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCont : MonoBehaviour
{
    public Transform target; // ссылка на объект, за которым следит камера
    public float smoothSpeed = 0.125f; // скорость перемещения камеры

    public float minZoom = 5f; // минимальное значение приближения
    public float maxZoom = 15f; // максимальное значение приближения
    public float zoomSpeed = 10f; // скорость приближения/отдаления

    private Vector3 offset; // расстояние между камерой и целью

    void Start()
    {
        offset = transform.position - target.position; // вычисляем расстояние между камерой и целью
    }

    void LateUpdate()
    {
        // Перемещение камеры
        Vector3 desiredPosition = target.position + offset; // вычисляем желаемую позицию камеры
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // сглаживаем перемещение камеры
        transform.position = smoothedPosition; // перемещаем камеру

        // Отдаление-приближение камеры
        float zoom = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed; // получаем значение приближения/отдаления от колеса мыши
        float desiredZoom = Camera.main.orthographicSize - zoom; // вычисляем желаемое приближение

        // Ограничиваем приближение в заданном диапазоне
        desiredZoom = Mathf.Clamp(desiredZoom, minZoom, maxZoom);

        // Плавно изменяем приближение камеры
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, desiredZoom, Time.deltaTime * zoomSpeed);
    }
}
