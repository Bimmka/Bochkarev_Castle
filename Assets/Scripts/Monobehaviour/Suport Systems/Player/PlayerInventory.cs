using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Tooltip("Максимальная вместимость инвентаря")]
    [SerializeField] private int maxItemCount = 10;
    
    private List<PlayerItem> items = new List<PlayerItem>();

    public static event Action<Item_SO, int> OnInventoryChanged;

    private void Awake()
    {
        LoadItems();

        ItemTip.OnItemDestroyed += RemoveItem;
    }
    private void OnDestroy()
    {
        SaveItems();
        ItemTip.OnItemDestroyed -= RemoveItem;
    }

    private void Start()
    {
        DisplayInventoryItems();
    }
    
    /// <summary>
    /// Мето для отображения итемов из сохранения
    /// </summary>
    private void DisplayInventoryItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            OnInventoryChanged?.Invoke(items[i].Item, items[i].Count);
        }
    }

    private void SaveItems()
    {
        DataSaver.SaveListToJson(Path.Combine(Application.dataPath, DataSaver.PlayerInventoryFileName), items);
    }

    private async void LoadItems()
    {
        items =  await DataSaver.LoadingJsonToList<PlayerItem>(Path.Combine(Application.dataPath, DataSaver.PlayerInventoryFileName));
        if (items.Count > 0)
            for (int i = 0; i < items.Count; i++)
            {
                AddItem(items[i].Item, items[i].Count);
            }
    }
    /// <summary>
    /// Добавляем предмет в инвентарь
    /// </summary>
    /// <param name="newItem">Добавляемый предмет</param>
    private void AddItem(Item_SO newItem)
    {
        PlayerItem playerItem = items.FirstOrDefault(item => item.Item == newItem);
        if (playerItem != null)
            playerItem.Count++;
        else
        {
            playerItem = new PlayerItem(newItem, 1);
            items.Add(playerItem);
        }
            
        OnInventoryChanged?.Invoke(playerItem.Item, playerItem.Count);

    }
    
    /// <summary>
    /// Добавляем предмет в инвентарь (используется при инициализации инвентаря)
    /// </summary>
    /// <param name="newItem">Предмет</param>
    /// <param name="count">Количество предмета</param>
    private void AddItem(Item_SO newItem, int count)
    {
        OnInventoryChanged?.Invoke(newItem, count);
    }
    
    /// <summary>
    /// Убираем предмет из инвентаря
    /// </summary>
    /// <param name="removedItem">Предмет, который хотим убрать</param>
    private void RemoveItem(Item_SO removedItem)
    {
        PlayerItem playerItem = items.FirstOrDefault(item => item.Item == removedItem);
        playerItem.Count--;
        OnInventoryChanged?.Invoke(playerItem.Item, playerItem.Count);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDropedItem dropedItem) && items.Count < maxItemCount)
        {
            AddItem(dropedItem.GetCurrentItem());
            dropedItem.DeinitItem();
        }
            
    }
}

[Serializable]
public class PlayerItem
{
    [SerializeField] private Item_SO item;
    [SerializeField] private int count;

    public Item_SO Item => item;

    public int Count
    {
        get => count;
        set => count = value;
    }

    public PlayerItem(Item_SO newItem, int itemCount)
    {
        item = newItem;
        count = itemCount;
    }
}
