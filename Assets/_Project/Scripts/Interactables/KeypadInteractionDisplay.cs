using UnityEngine;
using TMPro;

public class KeypadInteractionDisplay : MonoBehaviour
{
    [Header("UI Component References")]
    [SerializeField] private GameObject displayPanel;
    [SerializeField] private TextMeshProUGUI promptText;

    private void Awake()
    {
        // Start hidden
        HidePrompt();
    }

    public void ShowPrompt(string text)
    {
        if (promptText != null)
        {
            promptText.text = text;
        }

        if (displayPanel != null)
        {
            displayPanel.SetActive(true);
        }
    }

    public void HidePrompt()
    {
        if (displayPanel != null)
        {
            displayPanel.SetActive(false);
        }
    }
}