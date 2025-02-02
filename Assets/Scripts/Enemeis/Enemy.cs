using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyType type;
    public subEnemyType subType;
    
    public float Speed = 5;
    
    [Header("Enemy Patrol Settings")]
    public float Idle_Time = 5;
    
    [HideInInspector] public Transform Point_1;
    [HideInInspector] public Transform Point_2;
    
    
    int cap;
    bool isPatroling = true;

    [HideInInspector] public Transform player;
    
    [Header("Health Settings")]
    public int Health = 100;
    private int currentHealth;
    
    [Header("Animator Settings")]
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    
    [Header("Sub type settings")]
    [HideInInspector] public List<Transform> waypoints;
    
    [Header("Guns")]
    public Transform FirePoint;
    public Transform gunTransform;
    public Gun currentGun;
    
    
    [Header("Shooting Enemy")]
    private float muzzleOffset;
    public GameObject round;
    private int ammunition;
    private int remainingAmmunition;
    private float reloadTime;
    private float fireRate;
    private int roundsPerShot;
    private float Recoil;
    private float roundSpeed;
    private ShootState shootState = ShootState.Ready;
    private float nextShootTime = 0;
    public int damage = 1;
    [HideInInspector] public bool isShooting;

    [HideInInspector] public bool flip;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        muzzleOffset = GetComponent<Renderer>().bounds.extents.z;
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
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            if (bullet.Type == Bullet.ShooterType.Player)
            {
                TakeDamage(other.gameObject.GetComponent<Bullet>().Damage);
            }
            Destroy(other.gameObject);
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
    
    public void GetGun(Gun gun)
    {        
        ammunition = gun.Ammo;
        reloadTime = gun.reloadTime;
        fireRate = gun.fireRate;
        roundsPerShot = gun.bulletPerShot;
        Recoil = gun.recoil;
        roundSpeed = gun.bulletForce;
        gunTransform.GetComponent<SpriteRenderer>().sprite = gun.sprite;

        remainingAmmunition = gun.Ammo;
        if (gun.prefab != null)
        {
            round = gun.prefab;
        }
        currentGun = gun;
    }


    public void updateShooting(Transform ShootPoint)
    {
        if (isShooting)
        {
            if (shootState == ShootState.Ready)
            {
                Shoot(ShootPoint);
            }
            switch (shootState)
            {
                case ShootState.Shooting:
                    if (Time.time > nextShootTime)
                    {
                        shootState = ShootState.Ready;
                    }
                    break;
                case ShootState.Reloading:
                    if (Time.time > nextShootTime)
                    {
                        remainingAmmunition = ammunition;
                        shootState = ShootState.Ready;
                    }
                    break;
            }
            
        }
        
        if (remainingAmmunition < ammunition)
        {
            Reload();
        }
    }
    public void Shoot(Transform ShootPoint)
    {
        if (shootState == ShootState.Ready)
        {
            FirePointDirection fireDirection = ShootPoint.GetComponent<FirePointDirection>();
        
            for (int i = 0; i < roundsPerShot; i++)
            {
                Vector2 spreadVector = new Vector2(
                    Random.Range(-Recoil, Recoil), 
                    Random.Range(-Recoil, Recoil)
                );                
            
                GameObject spawnedRound = Instantiate(
                    round, 
                    ShootPoint.position + ShootPoint.forward * muzzleOffset, 
                    ShootPoint.rotation
                );

                Vector2 bulletDirection = fireDirection.GetFireDirection();
            
                Bullet bullet = spawnedRound.GetComponent<Bullet>();
                bullet.Type = Bullet.ShooterType.Enemy;
                bullet.Damage = damage;
            
                spawnedRound.GetComponent<Rigidbody2D>().velocity = 
                    (bulletDirection * roundSpeed) + spreadVector;
            }

            remainingAmmunition--;
        
            if (remainingAmmunition > 0)
            {
                nextShootTime = Time.time + (1 / fireRate);
                shootState = ShootState.Shooting;
            }
            else
            {
                Reload();
            }
        }
    }
    
    public void Reload()
    {
        if (shootState == ShootState.Ready)
        {
            nextShootTime = Time.time + reloadTime;
            shootState = ShootState.Reloading;
        }
    }

    public enum EnemyType
    {
        Patrol,
        Chase,
        Special,
    }
    
    public enum subEnemyType
    {
        Teleport,
        Roll,
    }
    public enum ShootState
    {
        Ready,
        Shooting,
        Reloading
    }
}
