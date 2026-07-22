using UnityEngine;
using UnityEngine.SceneManagement; // Required for loading scenes

public class EndMenuTrigger : MonoBehaviour
{
    [SerializeField] private GameObject endMenuCanvas;

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

            // Unlock the mouse cursor so the player can click buttons
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Pause game time optional
            Time.timeScale = 0f;
        }
    }

    // --- ADD THESE TWO PUBLIC METHODS FOR YOUR BUTTONS ---

    public void StartOver()
    {
        Time.timeScale = 1f; // Always unpause time first
        SceneManager.LoadScene("Level1(Drone)"); // Put your exact Level 1 scene name here!
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Unpause time before leaving
        SceneManager.LoadScene("MainMenu"); // Change "MainMenu" to your exact main menu scene name
    }
}