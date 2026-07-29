using UnityEngine;

public class PalletLure : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    private SignalToggle signalToggle;
    private Rigidbody playerRb;
    private bool playerInside;

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        signalToggle = GetComponent<SignalToggle>();

        if (signalToggle == null)
        {
            Debug.LogWarning("No SignalToggle found on " + gameObject.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (!other.CompareTag("Player"))
            return;

        playerInside = true;
        playerRb = other.GetComponent<Rigidbody>();

        // Activate Arsenal lure immediately
        if (signalToggle != null)
        {
            signalToggle.signalActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInside = false;
        playerRb = null;

        // Turn off Arsenal lure
        if (signalToggle != null)
        {
            signalToggle.signalActive = false;
        }

        // Stop sound
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    private void Update()
    {
        if (!playerInside || playerRb == null)
            return;

        if (audioSource == null || audioSource.clip == null)
            return;

        float speed = playerRb.linearVelocity.magnitude;

        if (speed > 0.1f)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.time = audioSource.clip.length * 0.4f;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

}