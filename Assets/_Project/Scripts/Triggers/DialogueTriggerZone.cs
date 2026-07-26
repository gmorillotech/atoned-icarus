using UnityEngine;

public class DialogueTriggerZone : MonoBehaviour
{
    [Header("Data to Display")]
    [SerializeField] private LevelData icarusDialogueData;

    [Header("Settings")]
    [SerializeField] private bool triggerOnce = true;
    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered && triggerOnce) return;

        // Check if the colliding object is the Player
        if (other.CompareTag("Player"))
        {
            LevelDialogue dialogueManager = FindFirstObjectByType<LevelDialogue>();
            if (dialogueManager != null && icarusDialogueData != null)
            {
                dialogueManager.ShowLevelDialogue(icarusDialogueData);
                hasTriggered = true;
            }
        }
    }
}