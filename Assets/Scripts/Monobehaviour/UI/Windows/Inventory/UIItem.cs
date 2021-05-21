using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс, отвечающий за отображение в UI предмета
/// </summary>
public class UIItem : MonoBehaviour, IUIItem
{
    [Tooltip("Куда отображать иконку предмета")]
    [SerializeField] protected Image icon;

    [Tooltip("Куда отображать количества предмета в слоте")] 
    [SerializeField] private Text countText;

    protected Item_SO currentItem;

    protected virtual void Awake()
    {
    }

    protected virtual void OnDestroy()
    {
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
    
    /// <summary>
    /// Метод для удаления текущего предмета
    /// </summary>
    /// <param name="item"></param>
    protected void RemoveItem(Item_SO item)
    {
        if (currentItem == item)
            currentItem = null;
    }
    
    /// <summary>
    /// Метод для выставления значений инициализации
    /// </summary>
    /// <param name="itemView">Иконка предмета</param>
    /// <param name="itemCount">Количество предметов</param>
    /// <param name="isIconActive">Будет ли активна иконка</param>
    /// <param name="isTextActive">Будет ли активно отображение количества предметов</param>
    protected virtual void SetValue(Sprite itemView, int itemCount = 0, bool isIconActive = true, bool isTextActive = true)
    {
        if (icon.enabled != isIconActive || countText.enabled != isTextActive)
            ChangeItemViewState(isIconActive, isTextActive);
        countText.text = itemCount.ToString();
        icon.sprite = itemView;
    }
    
    public void InitItem(Item_SO item, int itemCount = 0, bool isIconActive = true, bool isTextActive = true)
    {
        SetValue(item != null ? item.Icon : null, itemCount, isIconActive, isTextActive);
        currentItem = item;
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
    void InitItem(Item_SO item, int itemCount = 0, bool isIconActive = true, bool isTextActive = true);
    
    
    /// <summary>
    /// Метод для сокрытия предмета
    /// </summary>
    void DisableItem();

}
