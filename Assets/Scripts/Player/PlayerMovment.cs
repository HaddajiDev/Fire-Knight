using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public Joystick joystick;

    [SerializeField] private float speed = 10f;

    void Start()
    {
        
    }
    
    void Update()
    {
        float x = joystick.Horizontal;
        float y = joystick.Vertical;
        Vector3 movement = new Vector3(x, y, 0);
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
