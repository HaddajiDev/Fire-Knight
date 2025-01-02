using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController instance;
    public Enemy currentEnemy;
    public Room currentRoom;
    bool FastForward = false;
    void Awake()
    {
        instance = this;
    }
    

    public void isFastForward(bool check)
    {
        FastForward = check;
        
        if (check)
        {
            currentEnemy.Speed = currentEnemy.Speed * 2;
        }
        else
        {
            currentEnemy.Speed = currentEnemy.Speed / 2;
        }
    }
}
