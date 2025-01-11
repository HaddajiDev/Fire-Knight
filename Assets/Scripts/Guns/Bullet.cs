using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
