using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject container; 

    void Start()
    {
        Debug.Log("PauseMenu script is active and running!");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (container != null)
        {
            bool isCurrentlyActive = container.activeSelf;
            bool willBePaused = !isCurrentlyActive;

            container.SetActive(willBePaused);

            // Pause or Unpause Audio Global Listener (Freezes Icarus voice exactly where he was)
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

    public void ResumeButton()
    {
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
        PlayerInventory.HasTaserPersistent = false;
        SceneManager.LoadScene("MainMenu");
    }
}