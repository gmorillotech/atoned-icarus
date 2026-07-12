using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        // Check if a main GameManager already exists in DontDestroyOnLoad
        GameManager[] duplicates = FindObjectsByType<GameManager>(FindObjectsSortMode.None);

        if (duplicates.Length > 1)
        {
            if (gameObject.scene.name != "DontDestroyOnLoad")
            {
                // Find the main traveling manager
                GameManager travelingManager = null;
                foreach (var manager in duplicates)
                {
                    if (manager.gameObject.scene.name == "DontDestroyOnLoad")
                    {
                        travelingManager = manager;
                        break;
                    }
                }

                // Before this local scene clone self-destructs, handle the local doctor duplicate
                HandlePlayerTransition();

                Destroy(gameObject);
                return;
            }
        }

        // If this is the original GameManager from Level 1, make it immortal
        DontDestroyOnLoad(gameObject);
        
        // Setup the camera for the very first level
        InitialCameraSetup();
    }

void HandlePlayerTransition()
{
    PlayerController[] players = FindObjectsByType<PlayerController>(FindObjectsSortMode.None);

    PlayerController travelingDoctor = null;
    PlayerController localCloneDoctor = null;

    foreach (var doc in players)
    {
        if (doc.gameObject.scene.name == "DontDestroyOnLoad")
            travelingDoctor = doc;
        else
            localCloneDoctor = doc;
    }

    if (travelingDoctor != null && localCloneDoctor != null)
    {
        travelingDoctor.transform.position = localCloneDoctor.transform.position;

        Rigidbody travelingRb = travelingDoctor.GetComponent<Rigidbody>();
        if (travelingRb != null)
        {
            travelingRb.linearVelocity = Vector3.zero;
            travelingRb.angularVelocity = Vector3.zero;
        }

        // Trigger the movement update using the SceneManager logic
        travelingDoctor.DetermineMovementMode();

        // --- CAMERA CONTROLLER STAGE ---
        var cmCam = Object.FindFirstObjectByType<Unity.Cinemachine.CinemachineCamera>();
        if (cmCam != null)
        {
            cmCam.Follow = travelingDoctor.transform;

            // Check if the loaded scene is a side scroller
            string activeScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.ToLower();
            if (activeScene.Contains("side") || activeScene.Contains("scroller") || activeScene.Contains("vent") || activeScene.Contains("shaft"))
            {
                // FIX: Use a dynamic property approach to adjust Damping so Unity 6 doesn't reject the type name
                // This tells the camera to become infinitely "lazy" tracking left-to-right, effectively locking it!
                System.Type camType = cmCam.GetType();
                var positionProperty = camType.GetProperty("PositionControl") ?? camType.GetProperty("FollowControl");
                
                if (positionProperty != null)
                {
                    var componentInstance = positionProperty.GetValue(cmCam);
                    if (componentInstance != null)
                    {
                        var dampingField = componentInstance.GetType().GetField("Damping") ?? componentInstance.GetType().GetField("m_Damping");
                        if (dampingField != null)
                        {
                            // Set X damping to massive (index 0) and keep Y damping responsive (index 1)
                            dampingField.SetValue(componentInstance, new Vector3(99999f, 1f, 1f));
                        }
                    }
                }
            }
        }

        Destroy(localCloneDoctor.gameObject);
    }
}

    void InitialCameraSetup()
    {
        PlayerController mainDoctor = Object.FindFirstObjectByType<PlayerController>();
        if (mainDoctor != null)
        {
            DontDestroyOnLoad(mainDoctor.gameObject);

            var cmCam = Object.FindFirstObjectByType<Unity.Cinemachine.CinemachineCamera>();
            if (cmCam != null)
            {
                cmCam.Follow = mainDoctor.transform;
            }
        }
    }
}