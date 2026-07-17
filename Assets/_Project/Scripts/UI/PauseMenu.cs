using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject container; 

    void Start()
    {
        // This will print to your console as soon as you press Play
        // If this doesn't show up, your script is not actually active in the scene!
        Debug.Log("PauseMenu script is active and running!");
    }

    void Update()
    {
        // 1. Test standard input
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key detected by standard Input Manager!");
            TogglePause();
        }

        // 2. BACKUP TEST (Press 'P' instead of Escape)
        // If 'P' works but 'Escape' doesn't, your browser/editor/UI is hijacking Escape!
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("'P' key detected!");
            TogglePause();
        }
    }

    private void TogglePause()
    {
        if (container != null)
        {
            container.SetActive(true);
            Time.timeScale = 0;
            Debug.Log("Pause menu container activated successfully.");
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
            Time.timeScale = 1;
        }
    }

    public void MainMenuButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu"); 
    }
}