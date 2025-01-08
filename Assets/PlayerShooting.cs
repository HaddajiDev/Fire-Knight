using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    public static PlayerShooting instance;
    private void Awake()
    {
        instance = this;
    }
    private int damage;    

    [Header("Magazine")]
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

   

    public Transform ShootPoint;

    public SpriteRenderer GunSprite;

    
    private TMPro.TMP_Text ammunition_Text;
    public Gun current_Gun;


    
    [HideInInspector] public bool isShooting = false;

    [Header("Enemy Settings")]
    public float maxRange;
    private GameObject[] enemies;
    [HideInInspector] public GameObject closestEnemy;
    public float updateInterval = 0.1f;
    private float timer = 0f;


    void Start()
    {
        GetGun(current_Gun);
        muzzleOffset = GetComponent<Renderer>().bounds.extents.z;
        ammunition_Text =GameObject.Find("Ammunition").GetComponent<TextMeshProUGUI>();
    }

    public void GetGun(Gun gun)
    {        
        ammunition = gun.Ammo;
        reloadTime = gun.reloadTime;
        fireRate = gun.fireRate;
        roundsPerShot = gun.bulletPerShot;
        Recoil = gun.recoil;
        damage = gun.damage;
        roundSpeed = gun.bulletForce;
        GunSprite.sprite = gun.sprite;

        remainingAmmunition = gun.Ammo;
        if (gun.prefab != null)
        {
            round = gun.prefab;
        }

        current_Gun = gun;
    }

    void Update()
    {
        if (isShooting)
        {
            if (shootState == ShootState.Ready)
            {
                Shoot();
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
            ammunition_Text.text = $"{remainingAmmunition}/{ammunition}";
        }
        
        if (Input.GetKeyDown(KeyCode.R) && remainingAmmunition < ammunition)
        {
            Reload();
        }

        timer += Time.deltaTime;
        if (timer >= updateInterval)
        {
            timer = 0f;
            FindClosestEnemy();
        }
    }
    
    void FindClosestEnemy()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        closestEnemy = null;
        float closestDistanceSqr = maxRange * maxRange;

        foreach (GameObject enemy in enemies)
        {
            float distanceSqr = (enemy.transform.localPosition - transform.localPosition).sqrMagnitude;
            if (distanceSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                closestEnemy = enemy;
            }
        }
        
        if (closestEnemy == null)
        {
            closestEnemy = null;
        }
    }


    public void ShootEvent(bool check)
    {
        isShooting = check;
    }
    public void Shoot()
    {
        if (shootState == ShootState.Ready)
        {
            for (int i = 0; i < roundsPerShot; i++)
            {
                Vector3 spreadVector = new Vector3(Random.Range(-Recoil, Recoil), Random.Range(-Recoil, Recoil));                
                GameObject spawnedRound = Instantiate(round, ShootPoint.position + ShootPoint.forward * muzzleOffset, ShootPoint.rotation);
                Bullet bullet = spawnedRound.GetComponent<Bullet>();
                bullet.Damage = damage;
                spawnedRound.GetComponent<Rigidbody2D>().velocity = (ShootPoint.right * roundSpeed) + spreadVector; 
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
    public enum ShootState
    {
        Ready,
        Shooting,
        Reloading
    }
}   