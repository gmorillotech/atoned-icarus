using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject endMenuCanvas;

    private void OnTriggerEnter(Collider other)
    {
        // Detect when player enters the level finish area
        if (other.CompareTag("Player"))
        {
            OpenEndMenu();
        }
    }

    /// <summary>
    /// Call this method when the level ends (via trigger or script)
    /// </summary>
    public void OpenEndMenu()
    {
        // 1. Activate UI Canvas
        if (endMenuCanvas != null)
        {
            endMenuCanvas.SetActive(true);
        }

        // 2. Hide game HUD
        if (HUDController.Instance != null)
        {
            HUDController.Instance.HideHUD();
        }

        // 3. Unlock cursor for UI interaction
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // 4. Pause game time
        Time.timeScale = 0f;
    }

    public void StartOver()
    {
        ResetGameState();
        SceneManager.LoadScene("Level1(Drone)");
    }

    public void LoadMainMenu()
    {
        ResetGameState();
        SceneManager.LoadScene("MainMenu");
    }

    private void ResetGameState()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false; // Ensures global audio isn't paused in the next scene
        PlayerInventory.HasTaserPersistent = false;
    }
}