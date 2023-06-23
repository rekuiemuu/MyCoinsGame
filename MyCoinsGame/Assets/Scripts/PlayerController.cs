using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum ControlType
    {
        Mouse,
        WASD,
        Both
    }

    public ControlType controlType = ControlType.Mouse;
    public float speed = 5f;  
    public float shiftSpeedMultiplier = 2f;

    private void Update()
    {
        float moveSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            moveSpeed *= shiftSpeedMultiplier;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = Vector3.zero;

        if (controlType == ControlType.Mouse || controlType == ControlType.Both)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, transform.position.z));
            moveDirection = mousePosition - transform.position;
            moveDirection.z = 0;
            moveDirection.Normalize();
        }

        if (controlType == ControlType.WASD || controlType == ControlType.Both)
        {
            moveDirection += new Vector3(horizontalInput, verticalInput, 0);
            moveDirection.Normalize();
        }

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
