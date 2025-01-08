using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController instance;
    public Enemy currentEnemy;
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
            currentEnemy.Speed *= 2;
        }
        else
        {
            currentEnemy.Speed /= 2;
        }
    }

    public void isSlowMotion(bool check)
    {
        SlowDown = check;
        if (check)
        {
            currentEnemy.Speed /= 2;
        }
        else
        {
            currentEnemy.Speed *= 2;
        }
    }
}
