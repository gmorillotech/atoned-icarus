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

        if (other.CompareTag("Player"))
        {
            // Search scene including parents/HUD
            LevelDialogue dialogueManager = FindFirstObjectByType<LevelDialogue>(FindObjectsInactive.Include);

            if (dialogueManager != null && icarusDialogueData != null)
            {
                dialogueManager.ShowLevelDialogue(icarusDialogueData);
                hasTriggered = true;
            }
            else
            {
                Debug.LogWarning($"[DialogueTriggerZone] Missing LevelDialogue or LevelData on {gameObject.name}");
            }
        }
    }
}