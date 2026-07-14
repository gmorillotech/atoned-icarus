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
        if (!doc.gameObject.scene.IsValid() || doc.gameObject.scene.buildIndex == -1)
            travelingDoctor = doc;
        else
            localCloneDoctor = doc;
    }

    if (travelingDoctor == null && localCloneDoctor != null)
    {
        travelingDoctor = localCloneDoctor;
        localCloneDoctor = null; 
    }

    if (travelingDoctor != null)
    {
        if (localCloneDoctor != null)
        {
            travelingDoctor.transform.position = localCloneDoctor.transform.position;
        }

        Rigidbody travelingRb = travelingDoctor.GetComponent<Rigidbody>();
        if (travelingRb != null)
        {
            travelingRb.linearVelocity = Vector3.zero;
            travelingRb.angularVelocity = Vector3.zero;
        }

        // 1. Find the configuration object in the newly loaded scene
        var sceneConfig = Object.FindFirstObjectByType<SceneConfiguration>();

        // 2. Pass it directly to the player
        travelingDoctor.DetermineMovementMode(sceneConfig);

        // --- CAMERA CONTROLLER STAGE ---
        var cmCam = Object.FindFirstObjectByType<Unity.Cinemachine.CinemachineCamera>();
        if (cmCam != null)
        {
            cmCam.Follow = travelingDoctor.transform;

           if (sceneConfig != null && sceneConfig.LevelType == LevelType.SideScroller)
            {
                bool componentFound = false;

                // Use a reasonable damping speed (X: 1f, Y: 1f, Z: 1f) 
                // Lower numbers = camera catches up faster.
                Vector3 sideScrollDamping = new Vector3(1f, 1f, 1f); 

                var positionComposer = cmCam.GetComponent<Unity.Cinemachine.CinemachinePositionComposer>();
                if (positionComposer != null)
                {
                    positionComposer.Damping = sideScrollDamping;
                    componentFound = true;
                }
                
                if (!componentFound)
                {
                    var followComponent = cmCam.GetComponent<Unity.Cinemachine.CinemachineFollow>();
                    if (followComponent != null)
                    {
                        followComponent.TrackerSettings.PositionDamping = sideScrollDamping;
                        componentFound = true;
                    }
                }

                if (!componentFound)
                {
                    Debug.LogWarning($"[GameManager] HandlePlayerTransition: Found CinemachineCamera, but it is missing expected components.");
                }
            }
        }
        else
        {
            Debug.LogWarning("[GameManager] HandlePlayerTransition: Could not find a Unity.Cinemachine.CinemachineCamera in the scene!");
        }

        if (localCloneDoctor != null)
        {
            Destroy(localCloneDoctor.gameObject);
        }
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