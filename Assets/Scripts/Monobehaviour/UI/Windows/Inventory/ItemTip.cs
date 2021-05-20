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

    public static event Action<Item_SO> OnItemUsed;
    public static event Action<Item_SO> OnItemDestroyed; 
    private void Awake()
    {
        UIItem.OnItemClicked += SetTip;
        
        closeButton.onClick.AddListener(DisableTip);
        useButton.onClick.AddListener(UseItem);
        destroyButton.onClick.AddListener(DestroyItem);

        TryGetComponent(out rectTransform);
        
        DisableTip();
    }

    private void OnDestroy()
    {
        UIItem.OnItemClicked -= SetTip;
        
        closeButton.onClick.RemoveListener(DisableTip);
        useButton.onClick.RemoveListener(UseItem);
        destroyButton.onClick.RemoveListener(DestroyItem);
    }

    private void OnDisable()
    {
        DisableTip();
    }

    private void SetTip(RectTransform itemTransform, Item_SO item)
    {
        currentItem = item;
        rectTransform.localPosition = itemTransform.localPosition + new Vector3(rectTransform.rect.size.x * xOffset, 0,0);

        itemIcon.sprite = item.Icon;
        itemName.text = item.ItemName;
        ChangeGameObjectActiveState(true);
    }

    private void UseItem()
    {
        DisableTip();
        OnItemUsed?.Invoke(currentItem);
    }

    private void DestroyItem()
    {
        DisableTip();
        OnItemDestroyed?.Invoke(currentItem);
    }

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
