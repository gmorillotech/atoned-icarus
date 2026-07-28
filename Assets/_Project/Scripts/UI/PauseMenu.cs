using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject container;      // Main Pause Menu Panel
    public GameObject controlsPanel;  // Controls Keyboard Display Panel

    [Header("Audio Settings")]
    [SerializeField] private AudioSource musicAudioSource;     // Scene or AudioManager AudioSource
    [SerializeField] private AudioSource dialogueAudioSource;  // Dialogue Panel AudioSource
    [Range(0f, 1f)] [SerializeField] private float pausedMusicVolume = 0.3f; // Target volume when paused (30%)
    
    private float originalMusicVolume = 1f;

    void Start()
    {
        // 1. Try to find music source from persistent AudioManager if not assigned
        if (musicAudioSource == null && AudioManager.Instance != null)
        {
            musicAudioSource = AudioManager.Instance.GetComponent<AudioSource>();
        }

        // Store original music volume
        if (musicAudioSource != null)
        {
            originalMusicVolume = musicAudioSource.volume;
        }

        // 2. Try to find Dialogue AudioSource if not assigned in Inspector
        if (dialogueAudioSource == null)
        {
            GameObject dialogueObj = GameObject.Find("dialogue_panel");
            if (dialogueObj == null) dialogueObj = GameObject.Find("DialoguePanel");

            if (dialogueObj != null)
            {
                dialogueAudioSource = dialogueObj.GetComponent<AudioSource>();
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            // If Controls screen is open, ESC should close Controls and show Pause Menu
            if (controlsPanel != null && controlsPanel.activeSelf)
            {
                CloseControls();
            }
            else
            {
                TogglePause();
            }
        }
    }

    public void TogglePause()
    {
        if (container != null)
        {
            bool isCurrentlyActive = container.activeSelf;
            bool willBePaused = !isCurrentlyActive;

            // Make sure controls panel is closed if unpausing
            if (!willBePaused && controlsPanel != null)
            {
                controlsPanel.SetActive(false);
            }

            container.SetActive(willBePaused);

            // Handle Audio
            SetMusicDimmed(willBePaused);
            HandleDialoguePause(willBePaused);

            // Toggle HUD Visibility
            if (HUDController.Instance != null)
            {
                if (willBePaused)
                    HUDController.Instance.HideHUD();
                else
                    HUDController.Instance.ShowHUD();
            }

            Time.timeScale = willBePaused ? 0f : 1f;
        }
    }

    // Call this from the "CONTROLS" Button OnClick()
    public void OpenControls()
    {
        if (container != null) container.SetActive(false);
        if (controlsPanel != null) controlsPanel.SetActive(true);
    }

    // Call this from the Controls "BACK" Button OnClick()
    public void CloseControls()
    {
        if (controlsPanel != null) controlsPanel.SetActive(false);
        if (container != null) container.SetActive(true);
    }

    public void ResumeButton()
    {
        if (controlsPanel != null) controlsPanel.SetActive(false);

        if (container != null)
        {
            container.SetActive(false);
            
            // Restore Music Volume, Dialogue, & Time Scale
            SetMusicDimmed(false);
            HandleDialoguePause(false);
            Time.timeScale = 1f;

            if (HUDController.Instance != null)
            {
                HUDController.Instance.ShowHUD();
            }
        }
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f; // ALWAYS unfreeze time before loading scenes!
        SetMusicDimmed(false); // Restore normal volume levels
        HandleDialoguePause(false);
        PlayerInventory.HasTaserPersistent = false;
        SceneManager.LoadScene("MainMenu");
    }

    private void SetMusicDimmed(bool isDimmed)
    {
        if (musicAudioSource != null)
        {
            musicAudioSource.volume = isDimmed ? (originalMusicVolume * pausedMusicVolume) : originalMusicVolume;
        }
    }

    private void HandleDialoguePause(bool isPaused)
    {
        if (dialogueAudioSource == null) return;

        if (isPaused)
        {
            if (dialogueAudioSource.isPlaying)
            {
                dialogueAudioSource.Pause();
            }
        }
        else
        {
            // Unpause dialogue if it was previously playing
            dialogueAudioSource.UnPause();
        }
    }
}