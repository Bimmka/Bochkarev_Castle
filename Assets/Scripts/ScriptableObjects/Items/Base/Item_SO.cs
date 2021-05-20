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

    [Tooltip("Стакающийся ли предмет")] 
    [SerializeField] private ItemStuckType stuckType;

    [Tooltip("Иконка предмета")] 
    [SerializeField] private Sprite icon;

    public string ItemName => itemName;
    public ItemType Type => type;
    public ItemStuckType StuckType => stuckType;
    public Sprite Icon => icon;
}

[Serializable]
public enum ItemStuckType
{
    STUCKABLE,
    UNSTUCKABLE
}

[Serializable]
public enum ItemType
{
    WEAPON,
    AMMO,
    ARMOR,
    SUPPORT
}