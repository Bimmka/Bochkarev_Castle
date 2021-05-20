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
        
        for (int i = 0; i < items.Count; i++)
        {
            OnInventoryChanged?.Invoke(items[i].Item, items[i].Count);
        }

        ItemTip.OnItemDestroyed += RemoveItem;
    }

    private void OnDisable()
    {
        SaveItems();
    }

    private void OnDestroy()
    {
        ItemTip.OnItemDestroyed -= RemoveItem;
    }

    private void SaveItems()
    {
        DataSaver.SaveJson(Path.Combine(Application.dataPath, DataSaver.PlayerInventoryFileName), items);
    }

    private void LoadItems()
    {
        items = DataSaver.LoadingJson<List<PlayerItem>>(Path.Combine(Application.dataPath, DataSaver.PlayerInventoryFileName));
    }

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

public class PlayerItem
{
    private Item_SO item;
    private int count;

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
