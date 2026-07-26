using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // Persistent state across scenes (Taser only)
    public static bool HasTaserPersistent = false;

    // Reset per-scene automatically (Keycard stays in Level 1)
    public bool HasBlueCard { get; private set; }

    [Header("Keycard Config")]
    [SerializeField] private Sprite keycardSprite;

    [Header("Taser Config")]
    [SerializeField] private float taserCooldown = 6f;
    [SerializeField] private Sprite taserIcon;
    private float cooldownTimer = 0f;

    private void Start()
    {
        // When Level 3 loads, if we picked up the Taser earlier, activate its HUD element
        if (HasTaserPersistent && HUDController.Instance != null)
        {
            HUDController.Instance.DisplayTaser(taserIcon, "Taser");
        }
    }

    public void AddBlueCard()
    {
        HasBlueCard = true;

        if (HUDController.Instance != null)
        {
            HUDController.Instance.DisplayKeycard(keycardSprite, "Blue Keycard");
            HUDController.Instance.ShowDescriptionPopup("Item Acquired", "Picked up Blue Keycard");
        }
    }

    public void PickupTaser(Taser taser)
    {
        // Mark as acquired persistently across levels
        HasTaserPersistent = true;

        if (HUDController.Instance != null)
        {
            HUDController.Instance.DisplayTaser(taserIcon, "Taser");
            HUDController.Instance.ShowDescriptionPopup("Item Acquired", "Picked up Taser");
        }
    }

    void Update()
    {
        // Handle Taser recharge progress UI
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            float chargeProgress = 1f - (cooldownTimer / taserCooldown);

            if (HUDController.Instance != null)
            {
                HUDController.Instance.SetTaserEnergy(chargeProgress);
            }
        }

        // Fire Taser with F key in Level 3 (or any level after acquiring)
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (HasTaserPersistent)
            {
                if (cooldownTimer <= 0)
                {
                    cooldownTimer = taserCooldown;

                    if (HUDController.Instance != null)
                    {
                        HUDController.Instance.SetTaserEnergy(0f);
                    }

                    // Perform your Taser logic/attack here
                    Debug.Log("Taser Fired!");
                }
                else
                {
                    Debug.Log("Taser recharging: " + Mathf.Ceil(cooldownTimer) + "s");
                }
            }
        }
    }
}