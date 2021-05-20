using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс, отвечающий за отображение в UI предмета
/// </summary>
public class UIItem : MonoBehaviour, IUIItem
{
    [Tooltip("Куда отображать иконку предмета")]
    [SerializeField] private Image icon;

    [Tooltip("Куда отображать количества предмета в слоте")] 
    [SerializeField] private Text countText;

    [Tooltip("Кнопка, по которой будет открываться меню с информацией о предмете")] 
    [SerializeField] private Button infoButton;

    private Item_SO currentItem;

    public static event Action<RectTransform, Item_SO> OnItemClicked;

    private void Awake()
    {
        infoButton.onClick.AddListener(OnItemClick);
        ItemTip.OnItemDestroyed += RemoveItem;
    }

    private void OnDestroy()
    {
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
    
    /// <summary>
    /// Метод для изменения состояния картинки, текста
    /// </summary>
    /// <param name="isIconActive">Сделать ли иконку активной</param>
    /// <param name="isTextActive">Сделать ли текст активным</param>
    private void ChangeItemViewState(bool isIconActive, bool isTextActive)
    {
        icon.enabled = isIconActive;
        countText.enabled = isTextActive;
    }

    private void RemoveItem(Item_SO item)
    {
        if (currentItem == item)
            currentItem = null;
    }
    
    public void InitItem(Item_SO item, int itemCount, bool isIconActive = true, bool isTextActive = true)
    {
        if (icon.enabled != isIconActive || countText.enabled != isTextActive)
            ChangeItemViewState(isIconActive, isTextActive);

        currentItem = item;
        icon.sprite = currentItem.Icon;
        countText.text = itemCount.ToString();
    }

    public void DisableItem()
    {
        ChangeItemViewState(false, false);
    }
}

public interface IUIItem
{
    /// <summary>
    /// Метод для инициализации предмета
    /// </summary>
    /// <param name="item">Предмет</param>
    /// <param name="itemCount">Количество предметов</param>
    /// <param name="isIconActive">Будет ли активна иконка</param>
    /// <param name="isTextActive">Будет ли активно отображение количества предметов</param>
    void InitItem(Item_SO item, int itemCount, bool isIconActive = true, bool isTextActive = true);
    
    /// <summary>
    /// Метод для сокрытия предмета
    /// </summary>
    void DisableItem();
}
