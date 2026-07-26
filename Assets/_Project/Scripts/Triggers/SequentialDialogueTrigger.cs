using UnityEngine;

public class SequentialDialogueTrigger : MonoBehaviour
{
    [System.Serializable]
    public struct DialogueStep
    {
        public string speakerName;
        [TextArea(2, 5)] public string dialogueText;
        public bool hasTriggered;
    }

    [Header("Dialogue Sequence Settings")]
    [SerializeField] private DialogueStep[] dialogueSteps;
    
    private int currentStepIndex = 0;

    private void OnTriggerEnter(Collider other)
    {
        // Ensure only the Player activates the triggers
        if (!other.CompareTag("Player")) return;

        // Check if there are remaining dialogue steps
        if (currentStepIndex < dialogueSteps.Length)
        {
            DialogueStep currentStep = dialogueSteps[currentStepIndex];

            if (!currentStep.hasTriggered)
            {
                TriggerDialogue(currentStep);
                dialogueSteps[currentStepIndex].hasTriggered = true;
                currentStepIndex++;
            }
        }
    }

    private void TriggerDialogue(DialogueStep step)
    {
        // Display the popup / dialogue using HUDController or LevelDialogue manager
        if (HUDController.Instance != null)
        {
            HUDController.Instance.ShowDescriptionPopup(step.speakerName, step.dialogueText);
        }

        Debug.Log($"[{step.speakerName}]: {step.dialogueText}");
    }
}
