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
