using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private List<InventoryItem> startingItems = new List<InventoryItem>();

    public List<InventoryItem> CurrentItems { get; private set; } = new List<InventoryItem>();

    private void Awake()
    {
        CurrentItems = new List<InventoryItem>(startingItems);
    }

    private void Start()
    {
        inventoryUI.RefreshUI(CurrentItems);
    }

    public void SpawnItem(InventoryItem item)
    {
        if (!CurrentItems.Contains(item)) return;

        CurrentItems.Remove(item);

        GameObject spawnedObj = Instantiate(
            item.prefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        WorldItem worldItem = spawnedObj.GetComponent<WorldItem>();
        if (worldItem == null)
        {
            worldItem = spawnedObj.AddComponent<WorldItem>();
        }
        worldItem.Initialize(item, this);

        inventoryUI.RefreshUI(CurrentItems);
    }

    public void ReturnItem(InventoryItem item, GameObject worldObject)
    {
        CurrentItems.Add(item);

        Destroy(worldObject);

        inventoryUI.RefreshUI(CurrentItems);
    }
}