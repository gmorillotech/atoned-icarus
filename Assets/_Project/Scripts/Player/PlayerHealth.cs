using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public bool IsDead { get; private set; }
    private Rigidbody rb;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>(); // DEBUG
    }

    public void Die()
    {
        if (IsDead)
            return;

        IsDead = true;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        animator.SetTrigger("Die");
        Debug.Log("Player died!");
        Invoke(nameof(HandleDeath), 3f);
    }

    private void HandleDeath()
    {
        if (CheckpointManager.Instance != null)
        {
            CheckpointManager.Instance.LoadLastCheckpoint(gameObject);
        }

        IsDead = false;

        animator.ResetTrigger("Die");
        animator.Play("Blend Tree");
    }

}
