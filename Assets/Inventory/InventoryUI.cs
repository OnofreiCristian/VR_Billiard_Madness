using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private Transform itemGrid;
    [SerializeField] private Button itemButtonPrefab;

    public void RefreshUI(List<InventoryItem> itemsToDisplay)
    {
        foreach (Transform child in itemGrid)
        {
            Destroy(child.gameObject);
        }

        foreach (InventoryItem item in itemsToDisplay)
        {
            Button button = Instantiate(itemButtonPrefab, itemGrid);

            Image icon = button.transform.Find("Icon").GetComponent<Image>();
            TMP_Text itemName = button.transform.Find("ItemName").GetComponent<TMP_Text>();

            icon.sprite = item.icon;
            itemName.text = item.itemName;

            button.onClick.AddListener(() =>
            {
                inventoryManager.SpawnItem(item);
            });
        }
    }
}