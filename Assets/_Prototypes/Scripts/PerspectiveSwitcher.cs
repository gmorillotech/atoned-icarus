//imports
using UnityEngine;
using Unity.Cinemachine;

public class PerspectiveSwitcher : MonoBehaviour
{
    //just a way to stay organized
    [Header("Cameras")]

    //place to drop ur cameras into
    [SerializeField] private CinemachineCamera cam2D;
    [SerializeField] private CinemachineCamera cam3D;

    //false will start on 2D mode, how we know which camera is being used
    private bool is3DActive = false;

    //built-in unity methods
    //start forces us to start with 3D top-down persective at launch
    void Start()
    {
        SetPerspective(true);
    } //end start

    // 🌟 Runs automatically when the player enters a physics trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Make sure it's actually the Player stepping into the box
        if (other.CompareTag("Player"))
        {
            // When walking into this zone, force change to 2D Side-Scroller
            SetPerspective(false);
        }
    }

    // 🌟 Runs automatically when the player walks completely out of the zone
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Return back to standard 3D perspective when leaving the zone
            SetPerspective(true);
        }
    }
    

    private void SetPerspective(bool use3D)
    {
        //if 3D is higher, camera will switch, Cinemachine favors the highest number
        //starting a smooth blend to the 3D position
        if (use3D)
        {
            cam2D.Priority = 0;
            cam3D.Priority = 10;
            Debug.Log("Switched to 3D View"); //proving it worked
        }
        else
        {
            cam2D.Priority = 10;
            cam3D.Priority = 0;
            Debug.Log("Switched to 2D View"); //proving it moved and worked
        }
    } //end SetPerspective
}