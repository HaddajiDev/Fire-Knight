using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class item : ScriptableObject
{
    public Function _function;
    public string itemName;
    [TextArea] public string itemDescription;
    public Sprite icon;
    public int price;

    [Header("Value Settings")]
    public int value;
    [Header("Gun Setting")]
    public Gun gun;
    
    public enum Function
    {
        none,
        Health,
        Mana,
        SlowMotionTime,
        FastForwardTime,
        Gun,
    }
}
