using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckDeadEnd : MonoBehaviour
{
    private Collider2D myCollider;
    public GameObject DeadEndObj;
    void Start()
    {
        DeadEndObj.SetActive(true);
        myCollider = GetComponent<Collider2D>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            DeadEndObj.SetActive(false);
        }
    }
}
