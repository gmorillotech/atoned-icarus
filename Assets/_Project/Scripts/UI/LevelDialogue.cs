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

    [Header("Current Level Data")]
    [SerializeField] private LevelData currentLevelData;
    [SerializeField] private float displayDuration = 30f;

    private void Start()
    {
        ShowLevelDialogue();
    }

    public void ShowLevelDialogue()
    {
        if (currentLevelData == null) return;

        if (dialoguePanel == null) dialoguePanel = gameObject;

        if (speakerNameText != null) 
            speakerNameText.text = currentLevelData.speakerName;

        if (dialogueText != null) 
            dialogueText.text = currentLevelData.tutorialMessage;

        if (portraitImage != null && currentLevelData.speakerPortrait != null) 
            portraitImage.sprite = currentLevelData.speakerPortrait;

        if (dialogueAudio != null && currentLevelData.dialogueAudioClip != null)
        {
            dialogueAudio.PlayDialogueAudio(currentLevelData.dialogueAudioClip);
        }

        dialoguePanel.SetActive(true);
        StartCoroutine(HideDialogueAfterDelay(displayDuration));
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
    }
}