using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool isActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        // Adjust the tag check depending on what tag your Player object has (usually "Player")
        if (other.CompareTag("Player") && !isActivated)
        {
            isActivated = true;
            
            // Tell the manager to save this exact spot
            if (CheckpointManager.Instance != null)
            {
                CheckpointManager.Instance.SaveCheckpoint(transform.position);
            }
            
            // Optional visual feedback: turn the checkpoint green when activated
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.green;
            }
        }
    }
}
