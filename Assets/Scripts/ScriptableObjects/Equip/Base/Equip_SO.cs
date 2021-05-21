using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Equip", menuName = "Items / Create Equip")]
public class Equip_SO : ScriptableObject
{
    [SerializeField] private Equip[] equips;

    public Equip[] Equips => equips;

    public void SetNewItem(Item_SO item)
    {
        Equip currentEquip = equips.FirstOrDefault(equip => equip.ItemType == item.Type
                                                            && equip.Item == null);
        currentEquip.SetItem(item);
    }

    public bool IsNotUsedItem(Item_SO item)
    {
        return equips.FirstOrDefault(equip => equip.ItemType == item.Type
                                       && equip.Item == null) != null;
    }
}

[Serializable]
public class Equip
{
    [SerializeField] private Item_SO item;
    [SerializeField] private ItemType itemType;
    
    public Item_SO Item => item;
    public ItemType ItemType => itemType;

    public void SetItem(Item_SO newItem)
    {
        item = newItem;
    }
}
