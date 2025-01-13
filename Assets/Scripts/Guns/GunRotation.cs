using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour
{
    private Joystick joystick;
    public Transform Object;
    Vector2 GameobjectRotation;
    private float GameobjectRotation2;
    private float GameobjectRotation3;


    public bool FacingRight = true;

    [HideInInspector] public bool Cap;
    private bool isJoystickActive = false;
    private void Start()
    {
        joystick = GameObject.FindGameObjectWithTag("joystick").GetComponent<Joystick>();
        Cap = true;
    }

    void Update()
    {
        if (Object.rotation.z == 90 || Object.rotation.z == -90)
        {
            Object.Rotate(0, 180, 0);
        }
        
        if (PlayerShooting.instance.closestEnemy != null)
        {
            GameobjectRotation = (PlayerShooting.instance.closestEnemy.transform.position - Object.position).normalized;
            if (GameobjectRotation.x > 0 && !FacingRight)
            {
                Flip();
            }
            else if (GameobjectRotation.x < 0 && FacingRight)
            {
                Flip();
            }
            
            if (FacingRight)
            {
                GameobjectRotation2 = GameobjectRotation.x + GameobjectRotation.y * 90;
                Object.transform.rotation = Quaternion.Euler(0f, 0f, GameobjectRotation2);
            }
            else
            {
                GameobjectRotation2 = GameobjectRotation.x + GameobjectRotation.y * -90;
                Object.transform.rotation = Quaternion.Euler(0f, 180f, -GameobjectRotation2);
            }

        }
        else
        {
            GameobjectRotation = new Vector2(joystick.Horizontal, joystick.Vertical);


            GameobjectRotation3 = GameobjectRotation.x;

            isJoystickActive = GameobjectRotation.magnitude > 0.1f;

            if (isJoystickActive)
            {
                if (FacingRight)
                {
                    GameobjectRotation2 = GameobjectRotation.x + GameobjectRotation.y * 90;
                    Object.transform.rotation = Quaternion.Euler(0f, 0f, GameobjectRotation2);
                }
                else
                {
                    GameobjectRotation2 = GameobjectRotation.x + GameobjectRotation.y * -90;
                    Object.transform.rotation = Quaternion.Euler(0f, 180f, -GameobjectRotation2);
                }
            }
        }


        if (GameobjectRotation3 < 0 && FacingRight)
        {
            Flip();
        }
        else if (GameobjectRotation3 > 0 && !FacingRight)
        {
            Flip();
        }
    }
    public void Flip()
    {
        FacingRight = !FacingRight;
        Object.transform.Rotate(0, 180, 0);
    }
}
