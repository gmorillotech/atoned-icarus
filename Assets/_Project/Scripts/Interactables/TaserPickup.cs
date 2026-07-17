using UnityEngine;

public class TaserPickup : MonoBehaviour
{
    private bool playerNearby = false;
    private bool isPickedUp = false;

    private void OnTriggerEnter(Collider other)
    {

        if (isPickedUp)
            return;

        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            Debug.Log("Press E to pick up taser");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }

    void Update()
    {

        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            PlayerInventory inventory =
                FindFirstObjectByType<PlayerInventory>();

            inventory.PickupTaser(GetComponent<Taser>());

            Debug.Log("Taser picked up!");
            isPickedUp = true;

            gameObject.SetActive(false);
        }
    }
}