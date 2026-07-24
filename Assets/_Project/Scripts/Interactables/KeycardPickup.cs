using UnityEngine;

public class KeycardPickup : MonoBehaviour
{
    [Header("Item Details")]
    [SerializeField] private string itemName = "Blue Keycard";
    [SerializeField] private Sprite itemSprite; // Drag your keycard icon here in Inspector

    [Header("World Space UI")]
    [SerializeField] private GameObject interactionPromptUI; // Drag your World Space Canvas/Text here

    private bool playerNearby;

    private void Start()
    {
        // Ensure prompt is hidden when scene starts
        if (interactionPromptUI != null)
        {
            interactionPromptUI.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            
            // Show floating UI prompt above keycard
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

            // Hide floating UI prompt when player walks away
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
                inventory.AddBlueCard();

                // Update HUD inventory slot to show the keycard icon & name
                if (HUDController.Instance != null)
                {
                    HUDController.Instance.DisplayKeycard(itemSprite, itemName);
                }

                Destroy(gameObject);
            }
        }
    }
}