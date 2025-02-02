using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePointDirection : MonoBehaviour
{
    private Transform enemyParent;

    void Start()
    {
        enemyParent = transform.parent.parent;
    }

    public Vector2 GetFireDirection()
    {
        Vector2 direction = transform.right;
        if (enemyParent.localScale.x < 0)
        {
            direction *= -1;
        }
        
        return direction.normalized;
    }

}
