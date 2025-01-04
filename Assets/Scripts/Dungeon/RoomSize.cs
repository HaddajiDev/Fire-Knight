using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSize : MonoBehaviour
{
    public float Size;
    public Transform Walls;

    public Transform SpawnPoints;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
