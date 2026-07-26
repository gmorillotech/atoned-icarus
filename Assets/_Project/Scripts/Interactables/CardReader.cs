using UnityEngine;

public class CardReader : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private KeypadInteractionDisplay keypadDisplay;

    private bool playerNearby;
    private bool isUnlocked;

    private void Start()
    {
        // Auto-find display if attached to the same object
        if (keypadDisplay == null)
        {
            keypadDisplay = GetComponent<KeypadInteractionDisplay>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isUnlocked) return;

        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            UpdateDisplayPrompt(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            
            if (keypadDisplay != null)
            {
                keypadDisplay.HidePrompt();
            }
        }
    }

    private void Update()
    {
        if (!playerNearby || isUnlocked) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerInventory inventory = FindFirstObjectByType<PlayerInventory>();

            if (inventory != null && inventory.HasBlueCard)
            {
                Debug.Log("Access granted!");
                isUnlocked = true;

                if (keypadDisplay != null)
                {
                    keypadDisplay.HidePrompt();
                }

                if (doorAnimator != null)
                {
                    doorAnimator.SetTrigger("Open");
                }
                else
                {
                    Debug.LogWarning("[CardReader] doorAnimator not assigned!");
                }
            }
            else
            {
                Debug.Log("Access denied. Keycard required.");
                if (keypadDisplay != null)
                {
                    keypadDisplay.ShowPrompt("Requires Card");
                }
            }
        }
    }

    private void UpdateDisplayPrompt(GameObject playerObj)
    {
        if (keypadDisplay == null) return;

        PlayerInventory inventory = playerObj.GetComponent<PlayerInventory>();
        if (inventory == null)
        {
            inventory = FindFirstObjectByType<PlayerInventory>();
        }

        // Show "Press [E]" if they have the card, otherwise "Requires Card"
        if (inventory != null && inventory.HasBlueCard)
        {
            keypadDisplay.ShowPrompt("Press E");
        }
        else
        {
            keypadDisplay.ShowPrompt("Requires Card");
        }
    }
}