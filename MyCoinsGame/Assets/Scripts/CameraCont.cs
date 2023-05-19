/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCont: MonoBehaviour
{
    public Transform target; // ������ �� ������, �� ������� ������ ������
    public float smoothSpeed = 0.125f; // �������� ����������� ������

    private Vector3 offset; // ���������� ����� ������� � �����

    void Start()
    {
        offset = transform.position - target.position; // ��������� ���������� ����� ������� � �����
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset; // ��������� �������� ������� ������
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // ���������� ����������� ������
        transform.position = smoothedPosition; // ���������� ������
    }
}

*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCont : MonoBehaviour
{
    public Transform target; // ������ �� ������, �� ������� ������ ������
    public float smoothSpeed = 0.125f; // �������� ����������� ������

    public float minZoom = 5f; // ����������� �������� �����������
    public float maxZoom = 15f; // ������������ �������� �����������
    public float zoomSpeed = 10f; // �������� �����������/���������

    private Vector3 offset; // ���������� ����� ������� � �����

    void Start()
    {
        offset = transform.position - target.position; // ��������� ���������� ����� ������� � �����
    }

    void LateUpdate()
    {
        // ����������� ������
        Vector3 desiredPosition = target.position + offset; // ��������� �������� ������� ������
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // ���������� ����������� ������
        transform.position = smoothedPosition; // ���������� ������

        // ���������-����������� ������
        float zoom = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed; // �������� �������� �����������/��������� �� ������ ����
        float desiredZoom = Camera.main.orthographicSize - zoom; // ��������� �������� �����������

        // ������������ ����������� � �������� ���������
        desiredZoom = Mathf.Clamp(desiredZoom, minZoom, maxZoom);

        // ������ �������� ����������� ������
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, desiredZoom, Time.deltaTime * zoomSpeed);
    }
}
