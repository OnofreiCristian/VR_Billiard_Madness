using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private Transform itemGrid;
    [SerializeField] private Button itemButtonPrefab;
    [SerializeField] private List<InventoryItem> items;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (InventoryItem item in items)
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
