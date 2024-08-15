using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Швидкість руху
    public float maxSpeed = 10f; // Максимальна швидкість
    private Rigidbody2D rb;

    private Vector2 movement;

    void Start()
    {
        // Отримуємо компонент Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Отримуємо вхідні дані для руху
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // Виконуємо рух з використанням AddForce
        rb.AddForce(movement.normalized * moveSpeed, ForceMode2D.Impulse);

        // Обмеження швидкості
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
