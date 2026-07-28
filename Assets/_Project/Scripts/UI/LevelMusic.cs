using UnityEngine;

public class LevelMusic : MonoBehaviour
{
    [Header("Level Audio Settings")]
    [SerializeField] private AudioClip levelTrack;
    [Range(0f, 1f)] [SerializeField] private float levelVolume = 1f;
    [SerializeField] private bool loopTrack = true;

    private void Start()
    {
        if (AudioManager.Instance != null && levelTrack != null)
        {
            AudioManager.Instance.PlayMusic(levelTrack, levelVolume, loopTrack);
        }
    }
}
