using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Класс, овтечающий за панельку с снаряжением персонажа
/// </summary>
public class UIEquipPanel : MonoBehaviour
{
    private Dictionary<IUIEquipItem, IUIItem> equipItems = new Dictionary<IUIEquipItem, IUIItem>();

    private void Awake()
    {
        InitPanel();
        PlayerEquip.OnItemEquip += SetItem;
    }

    /// <summary>
    /// Инициализация панельки
    /// </summary>
    private void InitPanel()
    {
        UIEquipItem[] items = GetComponentsInChildren<UIEquipItem>(true).ToArray();
        for (int i = 0; i < items.Length; i++)
        {
            equipItems.Add(items[i].GetComponent<IUIEquipItem>(), items[i].GetComponent<IUIItem>());
        }

        ResetCells();
    }

    private void OnDestroy()
    {
        PlayerEquip.OnItemEquip -= SetItem;
    }
    
    /// <summary>
    /// Устанавливаем итем
    /// </summary>
    /// <param name="item">Итем, на котором нажали использовать</param>
    private void SetItem(Item_SO item)
    {
        KeyValuePair<IUIEquipItem, IUIItem> equipItem = equipItems.FirstOrDefault(currentItem 
                                                                          => currentItem.Key.GetCurrentItem() == null
                                                                          && currentItem.Key.GetItemType() == item.Type);
        
        if (equipItem.Key != null)
            equipItem.Value.InitItem(item, isTextActive:false);
    }
    
    /// <summary>
    /// Метод для сброса в дефолт всех ячеек
    /// </summary>
    private void ResetCells()
    {
        foreach (var item in equipItems)
        {
            item.Value.DisableItem();
        }
    }
}
