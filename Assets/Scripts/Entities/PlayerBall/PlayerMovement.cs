using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the ball movement
    public float threshold = 0.1f; // Threshold value to filter out minor fluctuations
    public float maxSpeed = 10f;

    private Vector3 initialOrientation;
    private Vector2 movement;
    private Rigidbody2D rb; // Reference to the Rigidbody2D component

    private bool _isPaused = false;
    private bool isMobile;

    void OnEnable()
    {
        LevelEvents.OnPause += Pause;
        // Capture the initial orientation of the device
        initialOrientation = Input.acceleration;

        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on the ball.");
        }
        isMobile = Application.isMobilePlatform;

    }
    private void OnDestroy()
    {
        LevelEvents.OnPause -= Pause;
    }

    void Update()
    {
       
        if (!_isPaused)
        {
            if (isMobile)
            {
                // Get the current orientation
                Vector3 currentOrientation = Input.acceleration;

                // Calculate the difference between the current and initial orientations
                Vector3 orientationDelta = currentOrientation - initialOrientation;

                // Calculate the magnitude of the orientation delta
                float magnitude = orientationDelta.magnitude;

                Vector2 movement = new Vector2(orientationDelta.x, orientationDelta.y).normalized;

                // Apply force to the Rigidbody2D
                rb.AddForce(movement * moveSpeed);
                // Limit the speed of the Rigidbody2D
                if (rb.velocity.magnitude > maxSpeed)
                {
                    rb.velocity = rb.velocity.normalized * maxSpeed;
                }
            }
            if (!isMobile)
            {
                // Get input data for movement from the keyboard
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");
                rb.AddForce(movement.normalized * moveSpeed, ForceMode2D.Impulse);

                // Limit the speed
                if (rb.velocity.magnitude > maxSpeed)
                {
                    rb.velocity = rb.velocity.normalized * maxSpeed;
                }
            }
        }
        else
        {
            rb.velocity = Vector3.zero;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Trap"))
        {
            LevelEvents.LoseLevel();
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("WinPoint"))
        {
            LevelEvents.WinLevel();
        }
    }

    private void Pause(bool isPaused)
    {
        _isPaused = isPaused;
    }
}
