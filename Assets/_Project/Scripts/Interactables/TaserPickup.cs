using UnityEngine;

public class TaserPickup : MonoBehaviour
{
    private bool playerNearby = false;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Pickup();
        }
    }

    private void Pickup()
    {
        Debug.Log("Taser picked up!");

        // Later: add to inventory
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
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
}