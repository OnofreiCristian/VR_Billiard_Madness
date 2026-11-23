using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class WorldItem : MonoBehaviour
{
    [Header("Input")]
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
        grabInteractable = GetComponent<XRGrabInteractable>();
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
    }
}