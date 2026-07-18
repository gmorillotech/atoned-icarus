using UnityEngine;

public class CardReader : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;
    private bool playerNearby;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            Debug.Log("Press E to use card reader");
            InteractionUI.Instance.ShowPrompt("Requires Keycard");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            InteractionUI.Instance.HidePrompt();
        }
    }

    private void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            PlayerInventory inventory = FindFirstObjectByType<PlayerInventory>();

            if (inventory != null)
            {
                if (inventory.HasBlueCard)
                {
                    Debug.Log("Access granted!");
                    InteractionUI.Instance.HidePrompt();
                    doorAnimator.SetTrigger("Open");
                }
                else
                {
                    Debug.Log("Access denied. Keycard required.");
                    InteractionUI.Instance.ShowPrompt("Requires Keycard");
                }
            }
        }
    }
}