using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
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

            // Pause game time
            Time.timeScale = 0f;
        }
    }

    public void StartOver()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1(Drone)"); 
    } 

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); 
    }
}