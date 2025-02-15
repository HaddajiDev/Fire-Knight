using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportEnemy : Enemy
{
    bool Teleporting = false;
    bool isTeleporting = false;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GetGun(currentGun);
    }
    private void Update()
    {
        if (!Teleporting)
        {
            StartCoroutine(in_out());
        }
        else
        {
            transform._mLookAt(player.transform, flip);
            if (!isTeleporting)
            {
                if (Vector3.Distance(player.transform.position, transform.position) < 5f)
                {
                    isShooting = true;
                    updateShooting(FirePoint);
                    animator.SetTrigger("idle");
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, player.position, Speed * Time.deltaTime);
                    animator.SetTrigger("walk");
                
                    isShooting = false;
                }
            }
        }
    }
    


    public void Teleport_in()
    {
        isShooting = false;
        isTeleporting = true;
        animator.SetTrigger("tp_in");
        GetComponent<BoxCollider2D>().enabled = false;
        gameObject.tag = "Respawn";
        Invoke(nameof(Teleport), Random.Range(2, 4));
    }

    public void Teleport()
    {
        transform.position = waypoints[Random.Range(0, waypoints.Count)].position;
        GetComponent<BoxCollider2D>().enabled = false;
        gameObject.tag = "Enemy";
        animator.SetTrigger("tp_out");
    }

    public void DisableSprite(int condition)
    {
        spriteRenderer.enabled = condition == 0 ? false : true;
    }

    public void DisableGunShooting(int condition) // 0 : true ::: 1 : false
    {
        gunTransform.GetComponent<SpriteRenderer>().enabled = condition == 0 ? true : false;
        isShooting = condition == 0 ? true : false;
        isTeleporting = condition == 0 ? false : true;
    }

    IEnumerator in_out()
    {
        Teleporting = true;
        yield return new WaitForSeconds(Random.Range(5, 10));
        Teleport_in();
        Teleporting = false;
    }
    

}
