using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSize : MonoBehaviour
{
    public float Size;
    public Transform Walls;

    public Transform SpawnPoints;

    public GameObject Holder;
    Room room;
    // Start is called before the first frame update
    void Start()
    {
        
        if (SpawnPoints != null && SpawnPoints != null)
        {
            Walls.localScale = new Vector3(Size, Size, 1);
            SpawnPoints.localScale = new Vector3(Size, Size, 1);
        }
        else
        {
            float currentSize = transform.localScale.x;
            transform.localScale = new Vector3(currentSize * Size, currentSize * Size, 1);
        }

        if (Holder != null)
        {
            Holder.transform.localScale = new Vector3(Size, Size, 1);
            for (int i = 0; i < Holder.transform.childCount; i++)
            {
                for (int j = 0; j < Holder.transform.GetChild(i).childCount; j++)
                {
                    Holder.transform.GetChild(i).transform.GetChild(j).transform.localScale = new Vector3(1 / Size, 1 / Size, 1);
                }
            }
        }

        Invoke(nameof(CheckForAll), 2f);

    }

    private void CheckForAll()
    {
        if (TryGetComponent<Room>(out Room room))
        {
            if (room.roomType == Room.RoomType.Boss)
            {
                CheckForRoomType("Boss");
            }
            else if (room.roomType == Room.RoomType.traps)
            {
                CheckForRoomType("Traps");
            }
            else if (room.roomType == Room.RoomType.shop)
            {
                CheckForRoomType("Shop");
            }
            else if (room.roomType == Room.RoomType.Regular)
            {
                CheckForRoomType("Regular");
            }
            else if(room.roomType == Room.RoomType.Empty)
            {
                CheckForRoomType("Empty");
            }
        }
    }

    private void CheckForRoomType(string roomType)
    {
        if (Holder != null)
        {
            for (int i = 0; i < Holder.transform.childCount; i++)
            {
                if (Holder.transform.GetChild(i).name == roomType)
                {
                    Holder.transform.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    Holder.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }
    void Update()
    {
        
    }
}
