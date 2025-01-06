using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Gun : ScriptableObject
{
    [Header("Main")]
    public Sprite sprite;
    public string Gunname;
    
    [Header("Gun Settings")]
    public GameObject prefab;
    public int Ammo;
    public int damage;
    public float bulletForce;
    public float fireRate;
    public float reloadTime;
    public int recoil;
    public int bulletPerShot;
}
