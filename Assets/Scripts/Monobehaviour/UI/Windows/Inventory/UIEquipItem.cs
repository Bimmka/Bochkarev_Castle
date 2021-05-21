using UnityEngine;

/// <summary>
/// Класс, отвечающий за иконки в окне снаряжения
/// </summary>
public class UIEquipItem : UIItem, IUIEquipItem
{
    [Tooltip("Базовый спрайт, если предмета нет в ячейке")]
    [SerializeField] private Sprite defaultView;

    [Tooltip("Какой тип предмета сюда подходит")]
    [SerializeField] private ItemType itemType;

    protected override void SetValue(Sprite itemView, int itemCount = 0, bool isIconActive = true, bool isTextActive = true)
    {
        base.SetValue(itemView, itemCount, isIconActive, isTextActive);
        
        if (icon.sprite == null)
            SetDefaultSprite();
    }

    private void SetDefaultSprite()
    {
        icon.sprite = defaultView;
    }

    public Item_SO GetCurrentItem()
    {
        return currentItem;
    }

    public ItemType GetItemType()
    {
        return itemType;
    }

}

public interface IUIEquipItem
{
    Item_SO GetCurrentItem();
    ItemType GetItemType();
}
