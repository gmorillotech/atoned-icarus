using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // This function will be called when the Play button is clicked
    public void PlayGame()
    {
        // Loads the level using its exact scene name
        SceneManager.LoadScene("Level1(Drone)");
    }

    // This function can be hooked up to a Quit button later if you want!
    public void QuitGame()
    {
        Debug.Log("Quit Game Clicked!");
        Application.Quit();
    }
}