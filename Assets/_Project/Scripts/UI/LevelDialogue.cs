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
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Default Level Start Data")]
    [SerializeField] private LevelData currentLevelData;
    [SerializeField] private float defaultDuration = 5f;

    private Coroutine activeDialogueCoroutine;

    private void Awake()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        if (currentLevelData != null)
        {
            ShowLevelDialogue(currentLevelData);
        }
    }

    public void ShowLevelDialogue()
    {
        ShowLevelDialogue(currentLevelData);
    }

    public void ShowLevelDialogue(LevelData data)
    {
        if (data == null) return;

        // Stop active timing/audio if another trigger is hit early
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

        // --- Calculate Dynamic Display Duration ---
        float finalDuration = defaultDuration;

        if (dialogueAudio != null && data.dialogueAudioClip != null)
        {
            dialogueAudio.PlayDialogueAudio(data.dialogueAudioClip);
            finalDuration = data.dialogueAudioClip.length; // Default to audio length
        }

        // Use custom display duration if specified on the ScriptableObject
        if (data.displayDuration > 0f)
        {
            finalDuration = data.displayDuration;
        }

        // Show UI via CanvasGroup
        dialoguePanel.SetActive(true);
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }

        activeDialogueCoroutine = StartCoroutine(HideDialogueAfterDelay(finalDuration));
    }

    private IEnumerator HideDialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        if (dialogueAudio != null)
        {
            dialogueAudio.StopAudio();
        }

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = false;
        }

        activeDialogueCoroutine = null;
    }
}