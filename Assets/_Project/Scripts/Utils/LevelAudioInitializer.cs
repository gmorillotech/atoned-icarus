using UnityEngine;

public class LevelAudioInitializer : MonoBehaviour
{
    [Header("Level Audio Settings")]
    [SerializeField] private AudioClip levelTrack;
    
    // Set max range higher (e.g., up to 2f for a boost)
    [Range(0f, 2f)] [SerializeField] private float levelVolume = 1f; 
    [SerializeField] private bool loopTrack = true;

    private void Start()
    {
        if (AudioManager.Instance != null && levelTrack != null)
        {
            AudioManager.Instance.PlayMusic(levelTrack, levelVolume, loopTrack);
        }
    }
}
