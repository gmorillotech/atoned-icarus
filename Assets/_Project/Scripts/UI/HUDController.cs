using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class HUDController : MonoBehaviour
{
    public static HUDController Instance; // Singleton to easily access from anywhere

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI objectiveText;
    [SerializeField] private TextMeshProUGUI activeItemText;
    [SerializeField] private Image activeItemIcon;

    [Header("Description Pop-up")]
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private TextMeshProUGUI popupTitleText;
    [SerializeField] private TextMeshProUGUI popupBodyText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void UpdateObjective(string newObjective)
    {
        if (objectiveText != null) objectiveText.text = newObjective;
    }

    public void UpdateActiveItem(string itemName, Sprite itemSprite)
    {
        if (activeItemText != null) activeItemText.text = itemName;
        if (activeItemIcon != null)
        {
            activeItemIcon.sprite = itemSprite;
            activeItemIcon.gameObject.SetActive(itemSprite != null);
        }
    }

    public void ShowDescriptionPopup(string title, string description, float duration = 4f)
    {
        StopAllCoroutines();
        StartCoroutine(PopupRoutine(title, description, duration));
    }

    private IEnumerator PopupRoutine(string title, string description, float duration)
    {
        popupTitleText.text = title;
        popupBodyText.text = description;
        popupPanel.SetActive(true);

        yield return new WaitForSecondsRealtime(duration);

        popupPanel.SetActive(false);
    }
}