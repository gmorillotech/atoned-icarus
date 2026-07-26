using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private GameObject endMenuCanvas;

    // REMOVED OnEnable() so the HUD isn't disabled when the scene loads!

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the Player
        if (other.CompareTag("Player"))
        {
            // Activate the End Menu Canvas
            if (endMenuCanvas != null)
            {
                endMenuCanvas.SetActive(true);
            }

            // Hide the HUD ONLY when reaching the level end trigger
            if (HUDController.Instance != null)
            {
                HUDController.Instance.HideHUD();
            }

            // Unlock the mouse cursor so the player can click buttons
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Pause game time
            Time.timeScale = 0f;
        }
    }

    public void StartOver()
    {
        Time.timeScale = 1f;
        PlayerInventory.HasTaserPersistent = false;
        SceneManager.LoadScene("Level1(Drone)");
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        PlayerInventory.HasTaserPersistent = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void TriggerDefeatScreen()
    {
        // Show your game over panel
        if (endMenuCanvas != null)
        {
            endMenuCanvas.SetActive(true);
        }
        else
        {
            gameObject.SetActive(true);
        }

        // Hide the HUD using the singleton instance
        if (HUDController.Instance != null)
        {
            HUDController.Instance.HideHUD();
        }
    }
}