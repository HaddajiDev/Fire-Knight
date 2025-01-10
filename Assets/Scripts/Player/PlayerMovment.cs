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

    private int lastFacingDirection = 1;
    bool flipPlayer, flipGun;
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

        if (PlayerShooting.instance.closestEnemy == null)
        {
            transform.localScale = new Vector3(currentScale.x * lastFacingDirection, currentScale.y, currentScale.z);
            GunTransform.localScale = new Vector3(0.5f * lastFacingDirection, 0.5f, 1);
            if (x > 0)
            {
                lastFacingDirection = 1;
            }
            else if (x < 0)
            {
                lastFacingDirection = -1;
            }
        }
        else
        {
            transform._mLookAt(PlayerShooting.instance.closestEnemy.transform, flipPlayer);
            lastFacingDirection = transform.localScale.x > 0 ? 1 : -1;
        }
        
        

        Vector3 movement = new Vector3(x, y, 0);
        transform.Translate(movement * speed * Time.deltaTime);
    }
    
    
}

public static class TransformExtensions
{
    public static void _mLookAt(this Transform transform, Transform target, bool flip = false)
    {
        Vector3 scale = transform.localScale;

        if (target.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? 1 : -1);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? 1 : -1);
        }

        transform.localScale = scale;
    }
}