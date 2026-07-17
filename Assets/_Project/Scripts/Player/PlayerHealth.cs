using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private bool isDead = false;

    public bool IsDead => isDead;

    public void Die()
    {
        if (isDead)
            return;

        isDead = true;

        Debug.Log("Player died!");

        HandleDeath();
    }

    private void HandleDeath()
    {
        // temporary
        gameObject.SetActive(false);
    }
}