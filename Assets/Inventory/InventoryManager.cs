using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private List<InventoryItem> startingItems = new List<InventoryItem>();

    // The live list of items currently inside the inventory
    public List<InventoryItem> CurrentItems { get; private set; } = new List<InventoryItem>();

    private void Awake()
    {
        // Copy starting items into our live inventory list
        CurrentItems = new List<InventoryItem>(startingItems);
    }

    private void Start()
    {
        inventoryUI.RefreshUI(CurrentItems);
    }

    public void SpawnItem(InventoryItem item)
    {
        if (!CurrentItems.Contains(item)) return;

        // 1. Remove from inventory data
        CurrentItems.Remove(item);

        // 2. Spawn into the world
        GameObject spawnedObj = Instantiate(
            item.prefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        // 3. Link the spawned object back to this inventory & its ScriptableObject data
        WorldItem worldItem = spawnedObj.GetComponent<WorldItem>();
        if (worldItem == null)
        {
            worldItem = spawnedObj.AddComponent<WorldItem>();
        }
        worldItem.Initialize(item, this);

        // 4. Update the UI grid
        inventoryUI.RefreshUI(CurrentItems);
    }

    public void ReturnItem(InventoryItem item, GameObject worldObject)
    {
        // 1. Put the item data back into the inventory list
        CurrentItems.Add(item);

        // 2. Remove the 3D object from the scene
        Destroy(worldObject);

        // 3. Update the UI grid so the button reappears
        inventoryUI.RefreshUI(CurrentItems);
    }
}