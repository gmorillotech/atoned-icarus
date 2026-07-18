using UnityEngine;

public class KeycardPickup : MonoBehaviour
{
    private bool playerNearby;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            Debug.Log("Press E to pick up keycard");
            InteractionUI.Instance?.ShowPrompt("Press E");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            InteractionUI.Instance?.HidePrompt();
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

                Debug.Log("Keycard picked up!");

                InteractionUI.Instance?.HidePrompt();

                Destroy(gameObject);
            }
        }
    }
}