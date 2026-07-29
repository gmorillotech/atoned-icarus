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
    [SerializeField] private AudioSource taserAudioSource; // <-- ADD THIS
    private float cooldownTimer = 0f;
    private Taser equippedTaser;
    [SerializeField] private GameObject heldTaser;
    [SerializeField] private Animator playerAnimator;

    private void Start()
    {
        // Restore taser after changing scenes
        if (HasTaserPersistent)
        {
            equippedTaser = GetComponent<Taser>();

            if (heldTaser != null)
            {
                heldTaser.SetActive(true);
            }

            if (HUDController.Instance != null)
            {
                HUDController.Instance.DisplayTaser(taserIcon, "Taser");
            }
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
        
        if (heldTaser != null)
        {
            heldTaser.SetActive(true);
        }

        equippedTaser = GetComponent<Taser>();

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
                    if (equippedTaser != null)
                    {
                        bool successfulHit = equippedTaser.Activate();

                        if (successfulHit)
                        {
                            if (playerAnimator != null)
                            {
                                playerAnimator.SetTrigger("ShootTaser");
                            }
                            
                            cooldownTimer = taserCooldown;

                            // PLAY TASER FIRING AUDIO
                            if (taserAudioSource != null)
                            {
                                taserAudioSource.Play();
                            }

                            if (HUDController.Instance != null)
                            {
                                HUDController.Instance.SetTaserEnergy(0f);
                            }
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Player has no equipped Taser reference!");
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