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
    
    public Transform PatrolPoint;
    
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.size = new Vector2(transform.parent.localScale.x * 3, transform.parent.localScale.y * 3);
    }

    private void Spawn()
{
    foreach (var spawnPoint in spawnPoints)
    {
        Enemy _enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity).GetComponent<Enemy>();
        _enemy.type = GetRandomEnemyType(Enemy.EnemyType.idle, Enemy.EnemyType.Chase, Enemy.EnemyType.Patrol);
        if (_enemy.type == Enemy.EnemyType.Patrol)
        {
            AssignPatrolPoints(_enemy, spawnPoint);
        }
        
        enemies.Add(_enemy);
    }
}

private void AssignPatrolPoints(Enemy enemy, Transform spawnPoint)
{
    EnemySpawnPoint point = spawnPoint.GetComponent<EnemySpawnPoint>();
    if (point == null || point.pos == null || point.pos.Count == 0)
    {
        return;
    }
    
    EnemySpawnPoint.AvailablePos selectedPos = point.pos[Random.Range(0, point.pos.Count)];
    Vector2 offset1 = Vector2.zero;
    Vector2 offset2 = Vector2.zero;

    switch (selectedPos)
    {
        case EnemySpawnPoint.AvailablePos.Top:
            offset1 = Vector2.zero;
            offset2 = new Vector2(0, 3);
            break;
        case EnemySpawnPoint.AvailablePos.Bottom:
            offset1 = Vector2.zero;
            offset2 = new Vector2(0, -3);
            break;
        case EnemySpawnPoint.AvailablePos.Right:
            offset1 = Vector2.zero;
            offset2 = new Vector2(3, 0);
            break;
        case EnemySpawnPoint.AvailablePos.Left:
            offset1 = Vector2.zero;
            offset2 = new Vector2(-3, 0);
            break;
    }
    Transform patrolPoint1 = Instantiate(PatrolPoint, enemy.transform.position + (Vector3)offset1, Quaternion.identity);
    Transform patrolPoint2 = Instantiate(PatrolPoint, enemy.transform.position + (Vector3)offset2, Quaternion.identity);

    enemy.Point_1 = patrolPoint1;
    enemy.Point_2 = patrolPoint2;
}

    
    public Enemy.EnemyType GetRandomEnemyType(params Enemy.EnemyType[] types)
    {
        Enemy.EnemyType[] validRoomTypes = types;
        int randomIndex = Random.Range(0, validRoomTypes.Length);
        return validRoomTypes[randomIndex];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!playerIn)
            {
                Spawn();
                playerIn = true;
            }
        }
    }
}
