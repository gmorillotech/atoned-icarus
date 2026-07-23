using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }

    [Header("Saved Data")]
    private Vector3 lastCheckpointPosition;
    private bool hasCheckpointSaved = false;

    private void Awake()
    {
        // Keep this object alive when switching scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Checkpoints don't carry over between levels
        hasCheckpointSaved = false;
    }

    // Call this when the player touches a checkpoint
    public void SaveCheckpoint(Vector3 position)
    {
        lastCheckpointPosition = position;
        hasCheckpointSaved = true;
        Debug.Log($"Checkpoint Saved at: {position}");
    }

    // Call this when the player dies
    public void LoadLastCheckpoint(GameObject player)
    {
        if (hasCheckpointSaved)
        {
            TeleportPlayer(player, lastCheckpointPosition);
            Debug.Log("Player teleported to last checkpoint!");
        }
        else
        {
            // If no checkpoint was touched yet, restart them at the start of the current level
            Debug.LogWarning("No checkpoint saved yet! Teleporting player back to level start.");
            ResetToLevelStart(player);
        }
    }

    private void ResetToLevelStart(GameObject player)
    {
        // Find the spawn point in the scene, or default to (0, 0, 0)
        GameObject spawnPoint = GameObject.FindWithTag("Respawn");
        Vector3 targetPos = spawnPoint != null ? spawnPoint.transform.position : Vector3.zero;
        
        TeleportPlayer(player, targetPos);
    }

    private void TeleportPlayer(GameObject player, Vector3 targetPosition)
    {
        // If your player uses a CharacterController, we must temporarily disable it 
        // to manually change transform.position, otherwise it will fight the teleport.
        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        player.transform.position = targetPosition;

        if (cc != null) cc.enabled = true;

        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}