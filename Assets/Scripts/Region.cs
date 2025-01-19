using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Region : ScriptableObject
{
    [Header("Enemies")]
    public GameObject[] GeneralEnemies;
    
    [Header("Region Enemies")]
    public GameObject[] EnemyRegions_1;
    public GameObject[] EnemyRegions_2;

    public enum RegionType
    {
        Region1,
        Region2,
    }
}
