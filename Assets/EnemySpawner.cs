using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int waveNumber = 1;
    public Transform[] spawnPoints;
    public List<Enemy> enemies;
    public GameObject enemyPrefab;
    
    BoxCollider2D boxCollider;

    private bool playerIn = false;
    
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.size = new Vector2(transform.parent.localScale.x * 3, transform.parent.localScale.y * 3);
    }

    private void Spawn()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Enemy _enemy = Instantiate(enemyPrefab, spawnPoints[i].position, Quaternion.identity).GetComponent<Enemy>();
            enemies.Add(_enemy);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!playerIn)
            {
                Spawn();
            }
            playerIn = true;
            
        }
    }
}
