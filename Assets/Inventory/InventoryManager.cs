using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnItem(InventoryItem item)
    {
        Instantiate(
            item.prefab,
            spawnPoint.position,
            spawnPoint.rotation
        );
    }
}
