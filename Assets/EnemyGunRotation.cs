using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunRotation : MonoBehaviour
{
    public Transform Object;
    Vector2 GameobjectRotation;
    private float GameobjectRotation2;
    private float GameobjectRotation3;
    
    public bool FacingRight = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Object._mLookAt(PlayerShooting.instance.transform, FacingRight);
        if (Object.rotation.z == 90 || Object.rotation.z == -90)
        {
            Object.Rotate(0, 180, 0);
        }
        
        GameobjectRotation = (PlayerShooting.instance.transform.position - Object.position).normalized;
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
