using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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

    private Animator animator;
    private bool isSprinting = false;
    private Rigidbody2D rb;
    private Vector2 moveDirection = Vector2.zero;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    private void Update()
    {
        float moveSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            moveSpeed *= shiftSpeedMultiplier;
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveDirection = Vector2.zero;

        if (controlType == ControlType.Mouse || controlType == ControlType.Both)
        {
            if (Input.GetMouseButton(1))
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, transform.position.z));
                moveDirection = mousePosition - transform.position;
                moveDirection.Normalize();
            }
        }

        if (controlType == ControlType.WASD || controlType == ControlType.Both)
        {
            moveDirection += new Vector2(horizontalInput, verticalInput);
            moveDirection.Normalize();
        }

        if (moveDirection.magnitude > 0)
        {
            animator.SetFloat("Speed", moveDirection.magnitude);
            animator.SetFloat("Vertical", moveDirection.y);
            animator.SetFloat("Horizontal", moveDirection.x);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime * (isSprinting ? shiftSpeedMultiplier : 1f));
    }
}
