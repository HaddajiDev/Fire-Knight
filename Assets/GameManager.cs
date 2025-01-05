using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public CinemachineVirtualCamera vcam;

    void Awake()
    {
        instance = this;
    }

    public void StartGame()
    {
        vcam.Follow = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
