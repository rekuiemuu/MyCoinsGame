using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(moveHorizontal, moveVertical);

        if (move.magnitude > 0)
        {
            transform.position += (Vector3)move * speed * Time.deltaTime;
            animator.SetFloat("Speed", move.magnitude);
            animator.SetFloat("Vertical", move.y);
            animator.SetFloat("Horizontal", move.x);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            move = (mousePos - transform.position).normalized;
            transform.position += (Vector3)move * speed * Time.deltaTime;
            animator.SetFloat("Speed", speed);
            animator.SetFloat("Vertical", move.y);
            animator.SetFloat("Horizontal", move.x);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }
}
