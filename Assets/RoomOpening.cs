using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOpening : MonoBehaviour
{

    public float m_value = 23;
    private void Start()
    {
        float value = 23;
        foreach (Transform child in transform)
        {
            if (child.localPosition.x > 0)
            {
                child.localPosition = new Vector3(value, child.localPosition.y, child.localPosition.z);
            }

            if (child.localPosition.x < 0)
            {
                child.localPosition = new Vector3(-value, child.localPosition.y, child.localPosition.z);
            }

            if (child.localPosition.y < 0)
            {
                child.localPosition = new Vector3(child.localPosition.x, -value, child.localPosition.z);
            }

            if (child.localPosition.y > 0)
            {
                child.localPosition = new Vector3(child.localPosition.x, value, child.localPosition.z);
            }

            RoomSpawner room = child.gameObject.GetComponent<RoomSpawner>();
            if(room != null)
                room.waitTime = 2;

        }
    }
}
