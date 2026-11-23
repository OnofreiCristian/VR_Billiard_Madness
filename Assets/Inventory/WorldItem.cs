using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class WorldItem : MonoBehaviour
{
    [SerializeField] private InputActionProperty stowButtonAction;

    private InventoryItem itemData;
    private InventoryManager inventoryManager;
    private XRGrabInteractable grabInteractable;

    public void Initialize(InventoryItem data, InventoryManager manager)
    {
        itemData = data;
        inventoryManager = manager;
    }

    private void Awake()
    {
        grabInteractable = GetComponentInParent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        if (stowButtonAction.action != null)
        {
            stowButtonAction.action.Enable();
            stowButtonAction.action.performed += OnStowButtonPressed;
        }
    }

    private void OnDisable()
    {
        if (stowButtonAction.action != null)
        {
            stowButtonAction.action.performed -= OnStowButtonPressed;
        }
    }

    private void OnStowButtonPressed(InputAction.CallbackContext context)
    {
        Debug.Log($"Stow button pressed on {name}! Grabbed: {(grabInteractable != null && grabInteractable.isSelected)}");

        if (grabInteractable != null && grabInteractable.isSelected)
        {
            StowBackToInventory();
        }
    }

    public void StowBackToInventory()
    {
        if (inventoryManager != null && itemData != null)
        {
            inventoryManager.ReturnItem(itemData, gameObject);
        }
        else
        {
            Debug.LogWarning("Cannot stow: InventoryManager or ItemData is missing!");
        }
    }
}