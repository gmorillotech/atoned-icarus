using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public bool IsDead { get; private set; }

    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip defaultDeathClip; // Gasp / Electrified sound

    private Rigidbody rb;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>(); // DEBUG

        // Automatically cache AudioSource if not manually assigned in Inspector
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    /// <summary>
    /// Call this to kill the player. Plays default death sound and animation.
    /// </summary>
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

        // Trigger Audio
        PlayDeathSound();

        // Play Death Animation
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        Debug.Log("Player died!");
        Invoke(nameof(HandleDeath), 3f);
    }

    private void PlayDeathSound()
    {
        if (audioSource != null && defaultDeathClip != null)
        {
            audioSource.PlayOneShot(defaultDeathClip);
        }
    }

    private void HandleDeath()
    {
        if (CheckpointManager.Instance != null)
        {
            CheckpointManager.Instance.LoadLastCheckpoint(gameObject);
        }

        IsDead = false;

        if (animator != null)
        {
            animator.ResetTrigger("Die");
            animator.Play("Blend Tree");
        }
    }
}