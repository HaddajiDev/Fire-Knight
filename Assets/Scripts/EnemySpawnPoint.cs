using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public List<AvailablePos> pos;
    public enum AvailablePos
    {
        Top,
        Bottom,
        Left,
        Right,
    }
}
