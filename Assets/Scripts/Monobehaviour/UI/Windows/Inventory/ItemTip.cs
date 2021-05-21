using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTip : MonoBehaviour
{
    [Header("Информация о предмете")]
    [Tooltip("Куда выводить иконку предмета")]
    [SerializeField] private Image itemIcon;

    [Tooltip("Куда выводить название предмета")] 
    [SerializeField] private Text itemName;
    
    [Header("Кнопки")]
    [Tooltip("Кнопка закрытия подсказки")]
    [SerializeField] private Button closeButton;

    [Tooltip("Кнопка использования предмета")]
    [SerializeField] private Button useButton;

    [Tooltip("Кнопка уничтожения предмета")]
    [SerializeField] private Button destroyButton;

    [Tooltip("Сдвиг вправо при показе"), Range(0, 1)] 
    [SerializeField] private float xOffset;
    
    private Item_SO currentItem;

    private RectTransform rectTransform;

    public static event Action<Item_SO, Action> OnItemUsed;
    public static event Action<Item_SO> OnItemDestroyed; 
    private void Awake()
    {
        UIInventoryItem.OnItemClicked += SetTip;
        
        closeButton.onClick.AddListener(DisableTip);
        useButton.onClick.AddListener(UseItem);
        destroyButton.onClick.AddListener(DestroyItem);

        TryGetComponent(out rectTransform);
        
        DisableTip();
    }

    private void OnDestroy()
    {
        UIInventoryItem.OnItemClicked -= SetTip;
        
        closeButton.onClick.RemoveListener(DisableTip);
        useButton.onClick.RemoveListener(UseItem);
        destroyButton.onClick.RemoveListener(DestroyItem);
    }

    private void OnDisable()
    {
        DisableTip();
    }
    
    /// <summary>
    /// Постановка подсказки
    /// </summary>
    /// <param name="itemTransform">Куда ставить</param>
    /// <param name="item">Какой итем выставляем</param>
    private void SetTip(RectTransform itemTransform, Item_SO item)
    {
        currentItem = item;
        rectTransform.localPosition = itemTransform.localPosition + new Vector3(rectTransform.rect.size.x * xOffset, 0,0);

        itemIcon.sprite = item.Icon;
        itemName.text = item.ItemName;
        ChangeGameObjectActiveState(true);
    }
    /// <summary>
    /// Использование предмета
    /// </summary>
    private void UseItem()
    {
        DisableTip();
        OnItemUsed?.Invoke(currentItem, () => DestroyItem());
    }
    /// <summary>
    /// Убираем предмет 
    /// </summary>
    private void DestroyItem()
    {
        DisableTip();
        OnItemDestroyed?.Invoke(currentItem);
    }
    
    /// <summary>
    /// Убираем подсказку
    /// </summary>
    private void DisableTip()
    {
        ChangeGameObjectActiveState(false);
    }
    
    private void ChangeGameObjectActiveState(bool isActive)
    {
        if (gameObject.activeSelf != isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
    
}
