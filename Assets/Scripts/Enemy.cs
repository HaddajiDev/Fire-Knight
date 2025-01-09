using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform Point_1;
    public Transform Point_2;
    
    public float Speed = 5;
    
    
    public float Idle_Time = 5;
    int cap;
    bool isPatroling = true;

    private void Update()
    {
        /*
        if (isPatroling && cap == 1)
        {
            Patrol_Point_2();
        }
        if (isPatroling && cap == 0)
        {
            Patrol_Point_1();
        }
        */
    }

    private void Patrol_Point_1()
    {
        transform.localPosition = Vector2.MoveTowards(transform.position, Point_1.position, Speed * Time.deltaTime);        
        if (transform.localPosition == Point_1.localPosition)
        {
            isPatroling = false;
            StartCoroutine(Wait(1));
        }
    }
    private void Patrol_Point_2()
    {
        transform.localPosition = Vector2.MoveTowards(transform.position, Point_2.position, Speed * Time.deltaTime);

        if (transform.localPosition == Point_2.localPosition)
        {
            isPatroling = false;
            StartCoroutine(Wait(0));
        }
    }
    IEnumerator Wait(int index)
    {
        yield return new WaitForSeconds(Idle_Time);
        isPatroling = true;
        cap = index;        
    }

}
