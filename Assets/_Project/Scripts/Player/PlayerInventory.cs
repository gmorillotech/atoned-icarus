using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool HasBlueCard { get; private set; }
    private Taser currentTaser;

    [Header("Keycard Config")]
    [SerializeField] private Sprite keycardSprite;

    [Header("Taser Config")]
    [SerializeField] private float taserCooldown = 5f;
    [SerializeField] private Sprite taserIcon;
    private float cooldownTimer = 0f;

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
        currentTaser = taser;

        if (HUDController.Instance != null)
        {
            HUDController.Instance.DisplayTaser(taserIcon, "Taser");
            HUDController.Instance.ShowDescriptionPopup("Item Acquired", "Picked up Taser");
        }
    }

    void Update()
    {
        // Handle Taser recharge progress
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            float chargeProgress = 1f - (cooldownTimer / taserCooldown);

            if (HUDController.Instance != null)
            {
                HUDController.Instance.SetTaserEnergy(chargeProgress);
            }
        }

        // Fire Taser with F key
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentTaser != null)
            {
                if (cooldownTimer <= 0)
                {
                    currentTaser.Activate();
                    cooldownTimer = taserCooldown;

                    if (HUDController.Instance != null)
                    {
                        HUDController.Instance.SetTaserEnergy(0f);
                    }
                }
                else
                {
                    Debug.Log("Taser recharging: " + Mathf.Ceil(cooldownTimer) + "s");
                }
            }
        }
    }
}