using UnityEngine;

public class LureInteractable : MonoBehaviour
{
    [Header("Lure Settings")]
    [SerializeField] private float activeDuration = 3f;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    [Header("World Space UI")]
    [SerializeField] private GameObject interactionPromptUI;

    private bool playerNearby;
    private bool isActive;
    private float activeTimer;

    private SignalToggle signalToggle;


    private void Start()
    {
        if (interactionPromptUI != null)
        {
            interactionPromptUI.SetActive(false);
        }

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
        if (other.CompareTag("Player"))
        {
            playerNearby = true;

            if (interactionPromptUI != null)
            {
                interactionPromptUI.SetActive(true);
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;

            if (interactionPromptUI != null)
            {
                interactionPromptUI.SetActive(false);
            }
        }
    }


    private void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            ToggleLure();
        }


        if (isActive)
        {
            activeTimer -= Time.deltaTime;

            if (activeTimer <= 0)
            {
                TurnOff();
            }
        }
    }


    private void ToggleLure()
    {
        if (isActive)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }
    }


    private void TurnOn()
    {
        isActive = true;
        activeTimer = activeDuration;

        if (signalToggle != null)
        {
            signalToggle.signalActive = true;
        }

        if (audioSource != null)
        {
            audioSource.Play();
        }

        Debug.Log(gameObject.name + " lure activated");
    }


    private void TurnOff()
    {
        isActive = false;

        if (signalToggle != null)
        {
            signalToggle.signalActive = false;
        }

        if (audioSource != null)
        {
            audioSource.Stop();
        }

        Debug.Log(gameObject.name + " lure deactivated");
    }
}