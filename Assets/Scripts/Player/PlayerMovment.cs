using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    private Joystick joystick;

    [SerializeField] private float speed = 10f;
    private Animator animator;
    private Vector3 currentScale;

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
        if (x != 0 || y != 0)
        {
            animator.SetTrigger("run");
        }
        else
        {
            animator.SetTrigger("idle");
        }

        if (x < 0)
        {
            transform.localScale = new Vector3(currentScale.x * -1, currentScale.y, currentScale.z);
        }
        else
        {
            transform.localScale = new Vector3(currentScale.x * 1, currentScale.y, currentScale.z);
        }
        Vector3 movement = new Vector3(x, y, 0);
        transform.Translate(movement * speed * Time.deltaTime);
    }
    

}
