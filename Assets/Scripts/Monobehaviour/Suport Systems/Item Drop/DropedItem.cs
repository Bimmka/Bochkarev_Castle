using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(BoxCollider2D))]
public class DropedItem : MonoBehaviour, IDropedItem
{
    [Header("Настройки вылета предмета")]
    [Tooltip("Диапазон значений силы, с которой вылетает предмет")]
    [SerializeField] private Vector2 forceValue;
    
    [Tooltip("Диапазон значений по X для задания направления вылета")]
    [SerializeField] private Vector2 xAxisDirection;
    
    [Tooltip("Диапазон значений по Y для задания направления вылета")]
    [SerializeField] private Vector2 yAxisDirection;
    
    [Tooltip("Rigidbody итема")]
    [SerializeField] private Rigidbody2D rigidbody; 
    
    [Header("Настройки отображения предмета")] 
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Item_SO currentItem;


    /// <summary>
    /// Дроп итема из врага
    /// </summary>
    /// <param name="startPosition">Стартовая позиция спавна</param>
    /// <param name="item">Итем, которым является данный предмет</param>
    private void DropItem(Vector3 startPosition, Item_SO item)
    {
        transform.position = startPosition;
        currentItem = item;
        spriteRenderer.sprite = currentItem.Icon;

        rigidbody.AddForce(CreateRandomDirection() * Random.Range(forceValue.x, forceValue.y), ForceMode2D.Impulse);
    }
    

    /// <summary>
    /// Создаем рандомное направление полета предмета
    /// </summary>
    /// <returns>Рандомное направление полета (Vector2)</returns>
    private Vector2 CreateRandomDirection()
    {
        Vector2 direction;
        direction.x = Random.Range(xAxisDirection.x, xAxisDirection.y);
        direction.y = Random.Range(yAxisDirection.x, yAxisDirection.y);
        return direction;
    }

    private void ChangeActiveState()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void InitItem(Vector3 startPosition, Item_SO item)
    {
        DropItem(startPosition, item);
        ChangeActiveState();
    }

    public void DeinitItem()
    {
        currentItem = null;
        ChangeActiveState();
    }

    public Item_SO GetCurrentItem()
    {
        return currentItem;
    }
}

public interface IDropedItem
{
    /// <summary>
    /// Инициализируем выпавший предмет
    /// </summary>
    /// <param name="startPosition">Точка, из которой вылетит предмет</param>
    /// <param name="item">Сам предмет</param>
    void InitItem(Vector3 startPosition, Item_SO item);
    
    /// <summary>
    /// Деинициализируем предмет
    /// </summary>
    void DeinitItem();
    
    /// <summary>
    /// Не используется ли сейчас предмет
    /// </summary>
    /// <returns>У предмета нет текущего отображаемого итема?</returns>
    Item_SO GetCurrentItem();
}
