using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FallHazardOverlay : MonoBehaviour
{
    public static FallHazardOverlay Instance { get; private set; }

    [Header("Fall Detection Settings")]
    [Tooltip("The exact Y-coordinate height threshold. Flashing starts when the player drops below this Y height.")]
    [SerializeField] private float dangerYLevel = -5f;

    [Header("UI References")]
    [Tooltip("Drag the RedOverlayImage from Canvas-HUD here.")]
    [SerializeField] private Image redOverlayImage;

    [Header("Flash Animation Settings")]
    [SerializeField] private float flashSpeed = 8f;
    [SerializeField] private float maxAlpha = 0.5f;

    private Transform playerTransform;
    private bool isFlashing = false;
    private Coroutine flashCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Find the player object in the scene automatically
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("FallHazardOverlay: No GameObject with tag 'Player' found in scene!");
        }

        // Auto-assign overlay image if it's attached to the same object
        if (redOverlayImage == null)
        {
            redOverlayImage = GetComponent<Image>();
        }
    }

    private void Update()
    {
        if (playerTransform == null || redOverlayImage == null) return;

        // Check player's Y position directly against the danger Y coordinate
        if (playerTransform.position.y <= dangerYLevel)
        {
            if (!isFlashing)
            {
                StartFlashing();
            }
        }
        else
        {
            if (isFlashing)
            {
                StopFlashing();
            }
        }
    }

    private void StartFlashing()
    {
        isFlashing = true;
        if (flashCoroutine != null) StopCoroutine(flashCoroutine);
        flashCoroutine = StartCoroutine(PulseRedImage());
    }

    public void StopFlashing()
    {
        isFlashing = false;

        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
            flashCoroutine = null;
        }

        if (redOverlayImage != null)
        {
            Color c = redOverlayImage.color;
            c.a = 0f;
            redOverlayImage.color = c;
        }
    }

    private IEnumerator PulseRedImage()
    {
        while (isFlashing)
        {
            if (redOverlayImage != null)
            {
                float alpha = (Mathf.Sin(Time.time * flashSpeed) + 1f) / 2f * maxAlpha;
                Color c = redOverlayImage.color;
                c.a = alpha;
                redOverlayImage.color = c;
            }
            yield return null;
        }
    }

    private void OnDisable()
    {
        StopFlashing();
    }
}