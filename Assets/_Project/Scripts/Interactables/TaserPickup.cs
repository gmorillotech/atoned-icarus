using UnityEngine;

public class TaserPickup : MonoBehaviour
{
    [Header("Item Details")]
    [SerializeField] private string itemName = "Taser";
    [SerializeField] private Sprite itemSprite; // Drag your taser UI icon here

    [Header("World Space UI")]
    [SerializeField] private GameObject interactionPromptUI; // Drag Canvas-PressE here

    private bool playerNearby = false;
    private bool isPickedUp = false;

    private void Start()
    {
        if (interactionPromptUI != null)
        {
            interactionPromptUI.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPickedUp) return;

        if (other.CompareTag("Player"))
        {
            playerNearby = true;

            if (interactionPromptUI != null)
            {
                interactionPromptUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;

            if (interactionPromptUI != null)
            {
                interactionPromptUI.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            PlayerInventory inventory = FindFirstObjectByType<PlayerInventory>();

            if (inventory != null)
            {
                Taser taser = GetComponent<Taser>();
                inventory.PickupTaser(taser);

                // Update HUD slot UI using the local name & sprite variables
                if (HUDController.Instance != null)
                {
                    HUDController.Instance.DisplayTaser(itemSprite, itemName);
                }
            }

            isPickedUp = true;
            gameObject.SetActive(false);
        }
    }
}