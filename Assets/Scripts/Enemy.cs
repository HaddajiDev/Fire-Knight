using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyType type;
    public Transform Point_1;
    public Transform Point_2;
    
    public float Speed = 5;
    
    
    public float Idle_Time = 5;
    int cap;
    bool isPatroling = true;
    
    [Header("Health Settings")]
    public int Health = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = Health;
    }

    private void Update()
    {
        if (type == EnemyType.Patrol)
        {
            if (isPatroling && cap == 1)
            {
                Patrol_Point_2();
            }
            if (isPatroling && cap == 0)
            {
                Patrol_Point_1();
            }
        }
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            TakeDamage(other.gameObject.GetComponent<Bullet>().Damage);
        }
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public enum EnemyType
    {
        Patrol,
        Chase,
        idle
    }

}
