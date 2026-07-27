using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainButtonsContainer; 
    public GameObject controlsPanel;         

    public void PlayGame()
    {
        PlayerInventory.HasTaserPersistent = false;
        SceneManager.LoadScene("Level1(Drone)");
    }

    public void OpenControls()
    {
        if (mainButtonsContainer != null) mainButtonsContainer.SetActive(false);
        if (controlsPanel != null) controlsPanel.SetActive(true);
    }

    public void CloseControls()
    {
        if (controlsPanel != null) controlsPanel.SetActive(false);
        if (mainButtonsContainer != null) mainButtonsContainer.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit!");
    }
}