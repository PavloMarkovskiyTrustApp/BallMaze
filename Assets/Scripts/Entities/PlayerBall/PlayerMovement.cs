using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // �������� ����
    public float maxSpeed = 10f; // ����������� ��������
    private Rigidbody2D rb;

    private Vector2 movement;

    void Start()
    {
        // �������� ��������� Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // �������� ����� ��� ��� ����
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // �������� ��� � ������������� AddForce
        rb.AddForce(movement.normalized * moveSpeed, ForceMode2D.Impulse);

        // ��������� ��������
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
