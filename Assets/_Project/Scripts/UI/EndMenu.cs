using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGoal : MonoBehaviour
{
    [Header("UI Settings")]
    [Tooltip("Drag your Win/End Screen UI panel here (if staying in the same scene).")]
    public GameObject endScreenContainer;

    [Header("Scene Transition Settings (Optional)")]
    [Tooltip("Check this if you want to load a completely separate Win Scene instead of showing a panel.")]
    public bool loadSeparateScene = false;
    public string nextSceneName = "MainMenu"; // Or "WinScene"

    private void OnTriggerEnter(Collider other)
    {
        // Adjust the tag check if your player tag is different
        if (other.CompareTag("Player"))
        {
            FinishLevel();
        }
    }

    // Backup for 2D games
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FinishLevel();
        }
    }

    private void FinishLevel()
    {
        Debug.Log("Player reached the final door!");

        if (loadSeparateScene)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(nextSceneName);
        }
        else if (endScreenContainer != null)
        {
            endScreenContainer.SetActive(true);
            Time.timeScale = 0f; // Freeze game on win
            
            // Unlock mouse cursor if locked
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Debug.LogError("LevelGoal error: End Screen Container is missing or Load Separate Scene is untagged!");
        }
    }
}
