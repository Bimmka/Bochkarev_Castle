using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Items", menuName = "Items / Create Item", order = 51)]
public class Item_SO : ScriptableObject
{
    [FormerlySerializedAs("name")]
    [Tooltip("Название предмета")]
    [SerializeField] private string itemName;
    
    [Tooltip("Тип предмета")]
    [SerializeField] private ItemType type;

    [Tooltip("Иконка предмета")] 
    [SerializeField] private Sprite icon;

    public string ItemName => itemName;
    public ItemType Type => type;
    public Sprite Icon => icon;
}

public enum ItemType
{
    WEAPON,
    AMMO,
    SHIELD,
    HELMET,
    SUPPORT
}
