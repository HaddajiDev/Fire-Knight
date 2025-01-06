using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Joystick joystick;

    [SerializeField] private float speed = 10f;
    private Animator animator;
    private Vector3 currentScale;
    public Transform GunTransform;

    private int lastFacingDirection = 1; // 1 for right, -1 for left

    void Start()
    {
        currentScale = transform.localScale;
        animator = GetComponent<Animator>();
        joystick = GameObject.FindGameObjectWithTag("joystick").GetComponent<Joystick>();
    }

    void Update()
    {
        float x = joystick.Horizontal;
        float y = joystick.Vertical;

        // Handle animations
        if (x != 0 || y != 0)
        {
            animator.SetTrigger("run");
        }
        else
        {
            animator.SetTrigger("idle");
        }

        // Update facing direction only when there is horizontal input
        if (x > 0)
        {
            lastFacingDirection = 1;
        }
        else if (x < 0)
        {
            lastFacingDirection = -1;
        }

        // Set scale based on the last facing direction
        transform.localScale = new Vector3(currentScale.x * lastFacingDirection, currentScale.y, currentScale.z);
        GunTransform.localScale = new Vector3(0.5f * lastFacingDirection, 0.5f, 1);

        // Move the player
        Vector3 movement = new Vector3(x, y, 0);
        transform.Translate(movement * speed * Time.deltaTime);
    }
}