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
            // Toggle container visibility based on current state
            bool isCurrentlyActive = container.activeSelf;
            container.SetActive(!isCurrentlyActive);

            // If it WAS active, we are resuming (Time = 1). Otherwise, we pause (Time = 0).
            Time.timeScale = isCurrentlyActive ? 1f : 0f;

            Debug.Log(isCurrentlyActive ? "Game Resumed." : "Game Paused.");
        }
        else
        {
            Debug.LogError("PauseMenu error: Container reference is missing in the Inspector!");
        }
    }

    public void ResumeButton()
    {
        if (container != null)
        {
            container.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f; // ALWAYS unfreeze time before loading scenes!
        SceneManager.LoadScene("MainMenu"); 
    }
}