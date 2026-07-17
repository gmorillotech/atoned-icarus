using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private bool isDead = false;

    public bool IsDead => isDead;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Die()
    {
        if (isDead)
            return;

        isDead = true;

        Debug.Log("Player died!");

        animator.SetTrigger("Die");

        Invoke(nameof(HandleDeath), 3f);
    }

    private void HandleDeath()
    {
        gameObject.SetActive(false);
    }
}