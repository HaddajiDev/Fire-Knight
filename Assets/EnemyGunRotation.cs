using UnityEngine;

public class EnemyGunRotation : MonoBehaviour
{
    private Transform player;
    private Transform enemyParent;
    private SpriteRenderer gunRenderer;

    public Transform firePoint;

    void Start()
    {
        enemyParent = transform.parent;
        gunRenderer = GetComponent<SpriteRenderer>();
        player = PlayerShooting.instance.transform;
    }

    void Update()
    {
        if (player == null) return;
        Vector3 direction = player.position - transform.position;
        bool parentFacingRight = enemyParent.localScale.x > 0;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (!parentFacingRight)
        {
            angle = 180 - angle;
            // angle *= -1;
        }
        transform.localRotation = Quaternion.Euler(0, 0, angle);
        
        firePoint.rotation = transform.rotation;

        gunRenderer.sortingOrder = enemyParent.GetComponent<SpriteRenderer>().sortingOrder + 1;
    }
}