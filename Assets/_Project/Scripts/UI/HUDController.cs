using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class HUDController : MonoBehaviour
{
    public static HUDController Instance { get; private set; }

    [Header("UI References - Objectives")]
    [SerializeField] private TextMeshProUGUI objectiveText;

    [Header("In Pockets UI - Container")]
    [SerializeField] private GameObject inPocketsContainer; // Parent UI object holding keycard, taser, etc.

    [Header("In Pockets UI - Keycard Slot")]
    [SerializeField] private GameObject keycardSlotGroup;
    [SerializeField] private Image keycardIcon;
    [SerializeField] private TextMeshProUGUI keycardText;

    [Header("In Pockets UI - Taser Slot")]
    [SerializeField] private GameObject taserSlotGroup;
    [SerializeField] private Image taserIcon;
    [SerializeField] private TextMeshProUGUI taserText;
    [SerializeField] private Slider taserEnergySlider;

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
        }
    }

    private void Start()
    {
        CheckLevelTypeAndSetPockets();
    }

    private void CheckLevelTypeAndSetPockets()
    {
        // Find the SceneConfiguration component in the scene
        SceneConfiguration sceneConfig = FindFirstObjectByType<SceneConfiguration>();

        if (sceneConfig != null)
        {
            // If the level is a SideScroller, hide the pocket container entirely
            bool shouldShowPockets = sceneConfig.LevelType != LevelType.SideScroller;

            if (inPocketsContainer != null)
            {
                inPocketsContainer.SetActive(shouldShowPockets);
            }
        }
    }

    public void UpdateObjective(string newObjective)
    {
        if (objectiveText != null) objectiveText.text = newObjective;
    }

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

    // Call when Taser is picked up
    public void DisplayTaser(Sprite sprite, string name = "Taser")
    {
        if (taserSlotGroup != null) taserSlotGroup.SetActive(true);
        if (taserIcon != null && sprite != null) taserIcon.sprite = sprite;
        if (taserText != null) taserText.text = name;

        // Show the energy slider when the taser is equipped
        if (taserEnergySlider != null) 
        {
            taserEnergySlider.gameObject.SetActive(true);
        }

        SetTaserEnergy(1f); // Full bar on pickup
    }

    // Call if the taser is dropped or removed
    public void HideTaser()
    {
        if (taserSlotGroup != null) taserSlotGroup.SetActive(false);
        if (taserEnergySlider != null) taserEnergySlider.gameObject.SetActive(false);
    }

    public void SetTaserEnergy(float fillAmount)
    {
        if (taserEnergySlider != null)
        {
            taserEnergySlider.value = Mathf.Clamp01(fillAmount);
        }
    }

    public void ShowPrompt(string message, float duration = 2f)
    {
        // Pass prompt messages to your popup panel
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

    public void HideHUD()
    {
        gameObject.SetActive(false);
    }

    public void ShowHUD()
    {
        gameObject.SetActive(true);
    }
}