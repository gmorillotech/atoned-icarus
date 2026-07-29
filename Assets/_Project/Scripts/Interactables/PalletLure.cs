using UnityEngine;

public class PalletLure : MonoBehaviour
{
    [Header("Lure Settings")]
    [SerializeField] private float activeDuration = 3f;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    private SignalToggle signalToggle;
    private Rigidbody playerRb;
    private bool playerInside;

    private bool isActive;

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        signalToggle = GetComponentInChildren<SignalToggle>();

        if (signalToggle == null)
        {
            Debug.LogWarning("No SignalToggle found on pallet: " + gameObject.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Pallet triggered by: " + other.name);
        
        if (!other.CompareTag("Player"))
            return;

        playerInside = true;
        playerRb = other.GetComponent<Rigidbody>();

        // Activate Arsenal lure immediately
        if (signalToggle != null)
        {
            signalToggle.signalActive = true;
            Debug.Log("SignalToggle activated on: " + signalToggle.gameObject.name);
        }
        else
        {
            Debug.LogWarning("SignalToggle is NULL on: " + gameObject.name);
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

        float speed = playerRb.linearVelocity.magnitude;

        if (speed > 0.1f)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.pitch = 1.1f;
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