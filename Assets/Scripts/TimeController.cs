using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController instance;
    private List<Enemy> currentEnemeis;
    public Room currentRoom;
    private bool FastForward = false;
    private bool SlowDown = false;
    void Awake()
    {
        instance = this;
    }
    

    public void isFastForward(bool check)
    {
        FastForward = check;
        
        if (check)
        {
            for (int i = 0; i < currentEnemeis.Count; i++)
            {
                currentEnemeis[i].Speed *= 2;
            }
        }
        else
        {
            for (int i = 0; i < currentEnemeis.Count; i++)
            {
                currentEnemeis[i].Speed /= 2;
            }
        }
    }

    public void isSlowMotion(bool check)
    {
        SlowDown = check;
        if (check)
        {
            for (int i = 0; i < currentEnemeis.Count; i++)
            {
                currentEnemeis[i].Speed /= 2;
            }
        }
        else
        {
            for (int i = 0; i < currentEnemeis.Count; i++)
            {
                currentEnemeis[i].Speed *= 2;
            }
        }
    }

    public void updateCurrentEnemies(List<Enemy> enemies)
    {
        currentEnemeis = enemies;
    }
}
