using TMPro;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    public static InteractionUI Instance;

    [SerializeField] private TextMeshProUGUI interactionText;

    private void Awake()
    {
        Instance = this;
        HidePrompt();
    }

    public void ShowPrompt(string message)
    {
        interactionText.text = message;
        interactionText.gameObject.SetActive(true);
    }

    public void HidePrompt()
    {
        interactionText.gameObject.SetActive(false);
    }
}