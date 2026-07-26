using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelDialogue : MonoBehaviour
{
    [Header("UI Component References")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text speakerNameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image portraitImage;
    [SerializeField] private DialogueAudio dialogueAudio; 

    [Header("Default Level Data")]
    [SerializeField] private LevelData currentLevelData;
    [SerializeField] private float displayDuration = 10f;

    private Coroutine activeDialogueCoroutine;

    private void Start()
    {
        if (currentLevelData != null)
        {
            ShowLevelDialogue(currentLevelData);
        }
    }

    /// <summary>
    /// Displays dialogue using the default LevelData set in the Inspector.
    /// </summary>
    public void ShowLevelDialogue()
    {
        ShowLevelDialogue(currentLevelData);
    }

    /// <summary>
    /// Displays dialogue dynamically using any supplied LevelData (e.g. from Triggers).
    /// </summary>
    public void ShowLevelDialogue(LevelData data)
    {
        if (data == null) return;

        // Stop active timing/audio if a new trigger is activated
        if (activeDialogueCoroutine != null)
        {
            StopCoroutine(activeDialogueCoroutine);
        }

        if (dialoguePanel == null) dialoguePanel = gameObject;

        if (speakerNameText != null) 
            speakerNameText.text = data.speakerName;

        if (dialogueText != null) 
            dialogueText.text = data.tutorialMessage;

        if (portraitImage != null && data.speakerPortrait != null) 
            portraitImage.sprite = data.speakerPortrait;

        if (dialogueAudio != null && data.dialogueAudioClip != null)
        {
            dialogueAudio.PlayDialogueAudio(data.dialogueAudioClip);
        }

        dialoguePanel.SetActive(true);
        activeDialogueCoroutine = StartCoroutine(HideDialogueAfterDelay(displayDuration));
    }

    private IEnumerator HideDialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        if (dialogueAudio != null)
        {
            dialogueAudio.StopAudio();
        }

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }

        activeDialogueCoroutine = null;
    }
}