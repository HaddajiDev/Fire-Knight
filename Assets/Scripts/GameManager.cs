using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Region.RegionType CurrentRegion;
    public CinemachineVirtualCamera vcam;
    
    [Header("coins")]
    public int coins;
    

    void Awake()
    {
        instance = this;
    }

    public void StartGame()
    {
        vcam.Follow = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void GetPlayerShoot(bool check)
    {
        PlayerShooting.instance.ShootEvent(check);
    }
}
