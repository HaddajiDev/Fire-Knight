#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(item))]
public class itemHelper : Editor
{
    private SerializedProperty value;
    private SerializedProperty gun;
    private SerializedProperty _function;
    private SerializedProperty itemDescription;
    private SerializedProperty itemName;
    private SerializedProperty itemPrice;
    private SerializedProperty itemIcon;
    
    private void OnEnable()
    {
        value = serializedObject.FindProperty("value");
        _function = serializedObject.FindProperty("_function");
        gun = serializedObject.FindProperty("gun");
        itemDescription = serializedObject.FindProperty("itemDescription");
        itemName = serializedObject.FindProperty("itemName");
        itemPrice = serializedObject.FindProperty("price");
        itemIcon = serializedObject.FindProperty("icon");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_function);
        
        EditorGUILayout.PropertyField(itemDescription);
        EditorGUILayout.PropertyField(itemName);
        EditorGUILayout.PropertyField(itemPrice);
        EditorGUILayout.PropertyField(itemIcon);
        switch ((item.Function)_function.enumValueIndex)
        {
            case item.Function.Gun:
                EditorGUILayout.PropertyField(gun);
                break;
            case item.Function.none:
                break;
            default:
                EditorGUILayout.PropertyField(value);
                break;
        }
        serializedObject.ApplyModifiedProperties();
    }
}

#endif
