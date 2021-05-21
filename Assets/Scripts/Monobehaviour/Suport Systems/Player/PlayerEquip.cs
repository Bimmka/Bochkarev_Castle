using System;
using UnityEngine;


/// <summary>
/// Класс, отвечающий за эквип персонажа
/// </summary>
public class PlayerEquip : MonoBehaviour
{
    [Tooltip("Итемы, которые на персонаже")] 
    [SerializeField] private Equip_SO equip;

    public static event Action<Item_SO> OnItemEquip;

    private void Awake()
    {
        ItemTip.OnItemUsed += UseItem;
    }

    private void OnDestroy()
    {
        ItemTip.OnItemUsed -= UseItem;
    }

    private void Start()
    {
        DisplayEquip(equip.Equips);
    }
    
    /// <summary>
    /// Отображение уже имеющихся итемов
    /// </summary>
    /// <param name="equipItems"></param>
    private void DisplayEquip(Equip[] equipItems)
    {
        for (int i = 0; i < equipItems.Length; i++)
        {
            if (equipItems[i].Item != null)
                OnItemEquip?.Invoke(equipItems[i].Item);
        }
    }

    
    /// <summary>
    /// Установка итема
    /// </summary>
    /// <param name="usedItem">Итем, который используют</param>
    /// <param name="actionOnUse">Дйствие, которое нужно выполнить после того, как поставим итем</param>
    private void UseItem(Item_SO usedItem, Action actionOnUse)
    {
        if (equip.IsNotUsedItem(usedItem))
        {
            equip.SetNewItem(usedItem);
            actionOnUse?.Invoke();
            OnItemEquip?.Invoke(usedItem);
        }
    }
}




