using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryItem : UIItem
{
    [Tooltip("Кнопка, по которой будет открываться меню с информацией о предмете")] 
    [SerializeField] private Button infoButton;


    public static event Action<RectTransform, Item_SO> OnItemClicked;
    
    protected override void Awake()
    {
        base.Awake();
        infoButton.onClick.AddListener(OnItemClick);
        ItemTip.OnItemDestroyed += RemoveItem;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        infoButton.onClick.RemoveListener(OnItemClick);
        ItemTip.OnItemDestroyed -= RemoveItem;
    }
    
    /// <summary>
    /// Метод, который вызывается при клике на предмет
    /// </summary>
    private void OnItemClick()
    {
        if (currentItem != null)
            OnItemClicked?.Invoke((RectTransform)transform, currentItem);
    }
    
}
