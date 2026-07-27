using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject container;      // Main Pause Menu Panel
    public GameObject controlsPanel;  // Controls Keyboard Display Panel

    void Start()
    {
        Debug.Log("PauseMenu script is active and running!");
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

            // Pause or Unpause Audio Global Listener
            AudioListener.pause = willBePaused;

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
            AudioListener.pause = false; // Resume Audio
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
        AudioListener.pause = false; // Unpause audio before scene switch
        PlayerInventory.HasTaserPersistent = false;
        SceneManager.LoadScene("MainMenu");
    }
}