using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    public static TimeController instance;
    private List<Enemy> currentEnemeis;
    [HideInInspector] public Room currentRoom;
    private bool FastForward = false;
    private bool SlowDown = false;
    
    public int FastMulitiplier = 2;
    public int SlowMultiplier = 2;
    public TMP_Text state;

    [Header("Settings")]
    public float TotalFastTime;
    public float TotalSlowTime;
    
    void Awake()
    {
        instance = this;
    }
    public void isFastForward(bool check)
    {
        FastForward = check;
        
        if (check && TotalFastTime > 0)
        {
            TotalFastTime -= Time.deltaTime;
            for (int i = 0; i < currentEnemeis.Count; i++)
            {
                currentEnemeis[i].Speed *= FastMulitiplier;
            }
            state.text = "Fast Forward";
        }
        else
        {
            for (int i = 0; i < currentEnemeis.Count; i++)
            {
                currentEnemeis[i].Speed /= FastMulitiplier;
            }
            state.text = "";
        }
    }

    public void isSlowMotion(bool check)
    {
        SlowDown = check;
        if (check && TotalSlowTime > 0)
        {
            TotalSlowTime -= Time.deltaTime;
            for (int i = 0; i < currentEnemeis.Count; i++)
            {
                currentEnemeis[i].Speed /= SlowMultiplier;
            }
            state.text = "Slow Motion";
        }
        else
        {
            for (int i = 0; i < currentEnemeis.Count; i++)
            {
                currentEnemeis[i].Speed *= SlowMultiplier;
            }
            state.text = "";
        }
    }

    public void updateCurrentEnemies(List<Enemy> enemies)
    {
        currentEnemeis = enemies;
    }
}
