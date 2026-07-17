using UnityEngine;

public class PlayerDeathTest : MonoBehaviour
{
    void Update()
    {
        // Press 'K' to simulate dying/killing the player
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("K pressed: Simulating Player Death...");
            TriggerDeath();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the player falls into a pit/hazard tagged "KillZone"
        if (other.CompareTag("KillZone"))
        {
            Debug.Log("Player fell into a Hazard!");
            TriggerDeath();
        }
    }

    public void TriggerDeath()
    {
        if (CheckpointManager.Instance != null)
        {
            CheckpointManager.Instance.LoadLastCheckpoint(gameObject);
        }
        else
        {
            Debug.LogError("No CheckpointManager found in the scene!");
        }
    }
}