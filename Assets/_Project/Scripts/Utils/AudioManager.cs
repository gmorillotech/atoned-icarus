using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Components")]
    [SerializeField] private AudioSource musicSource;

    [Header("Menu Settings")]
    [SerializeField] private AudioClip mainMenuMusic;
    [Range(0f, 1f)] [SerializeField] private float mainMenuVolume = 0.8f;

    private void Awake()
    {
        // Enforce Singleton Pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (musicSource == null)
            musicSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // Play Main Menu music on game launch
        PlayMenuMusic();
    }

    public void PlayMenuMusic()
    {
        PlayMusic(mainMenuMusic, mainMenuVolume, loop: true);
    }

    public void PlayMusic(AudioClip clip, float volume = 1f, bool loop = true)
    {
        if (clip == null) return;

        // Don't restart if the same clip is already playing
        if (musicSource.clip == clip && musicSource.isPlaying) return;

        musicSource.clip = clip;
        musicSource.volume = volume;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PauseMusic(bool pause)
    {
        if (pause) musicSource.Pause();
        else musicSource.UnPause();
    }
}