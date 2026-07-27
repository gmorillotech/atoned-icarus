using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
    public static HUDController Instance { get; private set; }

    [Header("Canvas Group Settings")]
    [SerializeField] private CanvasGroup hudCanvasGroup;

    [Header("UI References - Objectives")]
    [SerializeField] private TextMeshProUGUI objectiveText;

    [Header("In Pockets UI - Container")]
    [SerializeField] private GameObject inPocketsContainer;

    [Header("In Pockets UI - Keycard Slot")]
    [SerializeField] private GameObject keycardSlotGroup;
    [SerializeField] private Image keycardIcon;
    [SerializeField] private TextMeshProUGUI keycardText;

    [Header("Sub-Controllers")]
    [SerializeField] private TaserHUDUI taserUI; // Handled separately!

    [Header("Description Pop-up")]
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private TextMeshProUGUI popupTitleText;
    [SerializeField] private TextMeshProUGUI popupBodyText;

    private Coroutine popupCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (hudCanvasGroup == null)
        {
            hudCanvasGroup = GetComponent<CanvasGroup>();
        }
    }

    private void Start()
    {
        CheckLevelTypeAndSetPockets();
    }

    private void CheckLevelTypeAndSetPockets()
    {
        SceneConfiguration sceneConfig = FindFirstObjectByType<SceneConfiguration>();

        if (sceneConfig != null && inPocketsContainer != null)
        {
            // Explicitly checks the toggle set on the level's SceneConfiguration
            inPocketsContainer.SetActive(sceneConfig.showInPockets);
        }
    }

    // --- Objective API ---
    public void UpdateObjective(string newObjective)
    {
        if (objectiveText != null) 
            objectiveText.text = newObjective;
    }

    // --- Inventory / Keycard API ---
    public void UpdateActiveItem(string itemName, Sprite itemSprite)
    {
        DisplayKeycard(itemSprite, itemName);
    }

    public void DisplayKeycard(Sprite sprite, string name = "Keycard")
    {
        if (keycardSlotGroup != null) keycardSlotGroup.SetActive(true);
        if (keycardIcon != null && sprite != null) keycardIcon.sprite = sprite;
        if (keycardText != null) keycardText.text = name;
    }

    public void HideKeycard()
    {
        if (keycardSlotGroup != null) keycardSlotGroup.SetActive(false);
    }

    // --- Taser API Forwarding ---
    public void DisplayTaser(Sprite sprite, string name = "Taser")
    {
        if (taserUI != null) taserUI.DisplayTaser(sprite, name);
    }

    public void HideTaser()
    {
        if (taserUI != null) taserUI.HideTaser();
    }

    public void SetTaserEnergy(float fillAmount)
    {
        if (taserUI != null) taserUI.SetEnergy(fillAmount);
    }

    // --- Popup API ---
    public void ShowPrompt(string message, float duration = 2f)
    {
        ShowDescriptionPopup("Interact", message, duration);
    }

    public void ShowDescriptionPopup(string title, string description, float duration = 4f)
    {
        if (popupPanel == null) return;
        if (popupCoroutine != null) StopCoroutine(popupCoroutine);
        popupCoroutine = StartCoroutine(PopupRoutine(title, description, duration));
    }

    private IEnumerator PopupRoutine(string title, string description, float duration)
    {
        if (popupTitleText != null) popupTitleText.text = title;
        if (popupBodyText != null) popupBodyText.text = description;
        popupPanel.SetActive(true);

        yield return new WaitForSecondsRealtime(duration);

        popupPanel.SetActive(false);
        popupCoroutine = null;
    }

    // --- HUD Visibility API ---
    public void HideHUD()
    {
        if (hudCanvasGroup != null)
        {
            hudCanvasGroup.alpha = 0f;
            hudCanvasGroup.blocksRaycasts = false;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void ShowHUD()
    {
        if (hudCanvasGroup != null)
        {
            hudCanvasGroup.alpha = 1f;
            hudCanvasGroup.blocksRaycasts = true;
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}