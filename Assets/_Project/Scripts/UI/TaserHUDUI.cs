using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaserHUDUI : MonoBehaviour
{
    [Header("UI Component References")]
    [SerializeField] private GameObject taserSlotGroup;
    [SerializeField] private Image taserIcon;
    [SerializeField] private TextMeshProUGUI taserText;
    [SerializeField] private Slider taserEnergySlider;

    [Header("Default Visuals")]
    [SerializeField] private Sprite defaultTaserSprite; 

    [Header("Glow Settings")]
    [SerializeField] private Image sliderFillImage; 
    [SerializeField] private Color normalColor = Color.cyan;
    [Range(0.1f, 0.9f)]
    [SerializeField] private float dimMultiplier = 0.4f; // Dims down to 40% of normal brightness
    [SerializeField] private float pulseSpeed = 3f;

    private Coroutine glowCoroutine;
    private bool isFullyCharged = false;

    public void DisplayTaser(Sprite sprite, string name = "Taser")
    {
        gameObject.SetActive(true);

        if (taserSlotGroup != null && taserSlotGroup != gameObject) 
        {
            taserSlotGroup.SetActive(true);
        }

        // Use defaultTaserSprite if assigned in Inspector; otherwise fall back to passed sprite
        Sprite spriteToUse = (defaultTaserSprite != null) ? defaultTaserSprite : sprite;

        if (taserIcon != null && spriteToUse != null) 
        {
            taserIcon.sprite = spriteToUse;
        }

        if (taserText != null) taserText.text = name;

        if (taserEnergySlider != null) 
        {
            taserEnergySlider.gameObject.SetActive(true);
        }

        SetEnergy(1f); 
    }

    public void HideTaser()
    {
        StopGlow();

        if (taserEnergySlider != null) 
        {
            taserEnergySlider.gameObject.SetActive(false);
        }

        if (taserSlotGroup != null && taserSlotGroup != gameObject) 
        {
            taserSlotGroup.SetActive(false);
        }

        gameObject.SetActive(false);
    }

    public void SetEnergy(float fillAmount)
    {
        fillAmount = Mathf.Clamp01(fillAmount);

        if (taserEnergySlider != null)
        {
            taserEnergySlider.value = fillAmount;
        }

        if (fillAmount >= 0.99f)
        {
            if (!isFullyCharged)
            {
                isFullyCharged = true;
                StartGlow();
            }
        }
        else
        {
            if (isFullyCharged)
            {
                isFullyCharged = false;
                StopGlow();
            }
        }
    }

    private void StartGlow()
    {
        if (!gameObject.activeInHierarchy) return;

        if (glowCoroutine != null) StopCoroutine(glowCoroutine);
        glowCoroutine = StartCoroutine(GlowRoutine());
    }

    private void StopGlow()
    {
        if (glowCoroutine != null)
        {
            StopCoroutine(glowCoroutine);
            glowCoroutine = null;
        }

        if (sliderFillImage != null)
        {
            sliderFillImage.color = normalColor;
        }
    }

    private IEnumerator GlowRoutine()
    {
        if (sliderFillImage == null) yield break;

        // Calculate a dimmed variant of normalColor using dimMultiplier
        Color dimmedColor = new Color(
            normalColor.r * dimMultiplier,
            normalColor.g * dimMultiplier,
            normalColor.b * dimMultiplier,
            normalColor.a
        );

        while (isFullyCharged)
        {
            float t = (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f;
            sliderFillImage.color = Color.Lerp(dimmedColor, normalColor, t);
            yield return null;
        }
    }

    private void OnDisable()
    {
        StopGlow();
    }
}