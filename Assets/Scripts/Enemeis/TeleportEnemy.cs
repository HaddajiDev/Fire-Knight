using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportEnemy : Enemy
{
    [Header("Teleport")]
    public subEnemyType SubEnemyType = new subEnemyType();
    [Header("Teleport Settings")]
    public Transform[] waypoints;

    void Start()
    {
        Teleport_in();
    }

    public void Teleport_in()
    {
        animator.SetTrigger("tp_in");
        Invoke(nameof(Teleport), Random.Range(1, 3));
    }

    public void Teleport()
    {
        transform.position = waypoints[Random.Range(0, waypoints.Length)].position;
        animator.SetTrigger("tp_out");
    }

    public void DisableSprite(int condition)
    {
        spriteRenderer.enabled = condition == 0 ? false : true;
    }

}
