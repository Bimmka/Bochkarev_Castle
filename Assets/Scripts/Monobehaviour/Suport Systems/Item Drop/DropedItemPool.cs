using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DropedItemPool : MonoBehaviour
{
    [Tooltip("Prefab выпадающего предмета")]
    [SerializeField] private DropedItem dropedItemPrefab;
    
    private List<IDropedItem> items;

    private void Awake()
    {
        items = GetComponentsInChildren<IDropedItem>(true).ToList();
        ItemDropperFromEnemy.OnItemDrop += DropItem;
    }

    private void OnDestroy()
    {
        ItemDropperFromEnemy.OnItemDrop -= DropItem;
    }

    private void DropItem(Vector3 startPosition, Item_SO dropedItem)
    {
        IDropedItem dropItem = items.FirstOrDefault(item => item.GetCurrentItem() == null);
        if (dropItem == null)
        {
            dropItem = Instantiate(dropedItemPrefab, transform).GetComponent<IDropedItem>();
            items.Add(dropItem);
        }
        
        dropItem.InitItem(startPosition, dropedItem);
           
            
    }
}
