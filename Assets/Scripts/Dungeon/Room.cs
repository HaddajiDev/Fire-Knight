using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public RoomType roomType;
    public Transform SpawnPoints;
    public Enemy[] enemies;
    public enum RoomType
    {
        Empty,
        Regular, // with enemeis
        shop,
        Boss,
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TimeController.instance.currentRoom = this;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TimeController.instance.currentRoom = null;
        }
    }
}
