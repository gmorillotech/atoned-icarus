using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Taser currentTaser;

    [SerializeField] private float taserCooldown = 10f;
    private float cooldownTimer = 0f;

    void Update()
    {
        // Reduce cooldown timer
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentTaser != null)
            {
                if (cooldownTimer <= 0)
                {
                    currentTaser.Activate();

                    cooldownTimer = taserCooldown;
                }
                else
                {
                    Debug.Log(
                        "Taser is on cooldown: " 
                        + Mathf.Ceil(cooldownTimer) + "s"
                    );
                }
            }
        }
    }

    public void PickupTaser(Taser taser)
    {
        currentTaser = taser;
        Debug.Log("Taser stored!");
    }
}