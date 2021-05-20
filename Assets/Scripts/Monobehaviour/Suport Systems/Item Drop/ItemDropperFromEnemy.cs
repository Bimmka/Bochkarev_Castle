using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemDropperFromEnemy : MonoBehaviour
{
    [Tooltip("Предметы, которые могут выпасть")]
    [SerializeField] private DroppedItemStats[] droppedItems;

    [Tooltip("Объект, из которого выпадает дроп")]
    [SerializeField] private SpriteRenderer fromDrop;

    private float disapearTime = 1.5f;

    public static event Action<Vector3, Item_SO> OnItemDrop; 
    private void Start()
    {
        for (int i = 0; i < droppedItems.Length; i++)
        {
            CreateDropedItem(droppedItems[i]);
        }

        DisableVFX();
    }

    private void CreateDropedItem(DroppedItemStats item)
    {
        if (Random.Range(0f,1f) >= (1 - item.ChanceDrop))
            OnItemDrop?.Invoke(transform.position, item.Item);
    }

    private void DisableVFX()
    {
        fromDrop.DOFade(0f, disapearTime).SetEase(Ease.InOutSine).OnComplete(() => Destroy(gameObject));
    }
}

[Serializable]
public struct DroppedItemStats
{
    [Tooltip("Какой итем выпадет")]
    [SerializeField] private Item_SO item;
    
    [Tooltip("Шанс выпадения итема")]
    [SerializeField, Range(0,1)] private float chanceToDrop;

    public Item_SO Item => item;
    public float ChanceDrop => chanceToDrop;
}