using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Класс UI окна инвентаря 
/// </summary>
[RequireComponent(typeof(Canvas))]
public class UIInventory : MonoBehaviour
{
   [Header("Основные взаимодействия с окном инвентаря")]
   [Tooltip("Кнопка открытия инвентаря")]
   [SerializeField] private Button openButton;

   [Tooltip("Кнопка закрытия инвентаря")]
   [SerializeField] private Button closeButton;

   private IUIItem[] items;

   private Dictionary<IUIItem, Item_SO> displayedItems = new Dictionary<IUIItem, Item_SO>();

   private Canvas canvas;

   private void Awake()
   {
      TryGetComponent(out canvas);
      items = GetComponentsInChildren<IUIItem>(true).ToArray();
      openButton.onClick.AddListener(OpenWindow);
      closeButton.onClick.AddListener(CloseWindow);

      PlayerInventory.OnInventoryChanged += DisplayItem;
   }

   private void OnDestroy()
   {
      openButton.onClick.RemoveListener(OpenWindow);
      closeButton.onClick.RemoveListener(CloseWindow);
      
      PlayerInventory.OnInventoryChanged -= DisplayItem;
   }
   /// <summary>
   /// Метод для включения канваса
   /// </summary>
   private void OpenWindow()
   {
      ChangeCanvasActiveState(true);
   }
   
   /// <summary>
   /// Метод для отключения канваса
   /// </summary>
   private void CloseWindow()
   {
      ChangeCanvasActiveState(false);
   }
   
   /// <summary>
   /// Метод для смены состояния канваса
   /// </summary>
   /// <param name="isEnable">Сделать ли канвас активным</param>
   private void ChangeCanvasActiveState(bool isEnable)
   {
      canvas.enabled = isEnable;
   }

   private void DisplayItem(Item_SO item, int count)
   {
      IUIItem itemDisplay = GetCellForItem(item);
      if (itemDisplay == null)
      {
         itemDisplay = items.FirstOrDefault(cell => !displayedItems.ContainsKey(cell));
         displayedItems.Add(itemDisplay, item);
      }
      if (count > 0)
         itemDisplay.InitItem(item, count);
      else
         itemDisplay.DisableItem();
      
   }

   private IUIItem GetCellForItem(Item_SO item)
   {
      return displayedItems.FirstOrDefault(pair => pair.Key != null && pair.Value == item).Key;
   }
   
}
