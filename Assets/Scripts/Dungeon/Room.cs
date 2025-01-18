using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public RoomType roomType;
    public bool CornerRoom = false;
    
    public enum RoomType
    {
        Empty,
        Regular, // with enemeis
        traps,
        shop,
        Boss,
    }


    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            RoomSize roomSize = GetComponent<RoomSize>();
            if (roomType == RoomType.Regular)
            {
                if (roomSize.Holder != null)
                {
                    EnemySpawner enemySpawner = roomSize.Holder.GetComponentInChildren<EnemySpawner>();
                    TimeController.instance.updateCurrentEnemies(enemySpawner.enemies);
                }
            }
            TimeController.instance.currentRoom = this;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (roomType == RoomType.Regular)
            {
                TimeController.instance.updateCurrentEnemies(null);
            }
            TimeController.instance.currentRoom = null;
        }
    }
}
