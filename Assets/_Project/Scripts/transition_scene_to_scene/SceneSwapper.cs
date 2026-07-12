using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapper : MonoBehaviour
{
    [SerializeField] private string sceneName;

    // CHANGED: Uses 3D trigger collision instead of 2D
    private void OnTriggerEnter(Collider other)
    {
        // Checks if the object walking into the door has the PlayerController script
        if (other.GetComponent<PlayerController>() != null)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}