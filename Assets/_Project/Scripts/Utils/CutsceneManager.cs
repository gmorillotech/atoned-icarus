using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [Header("UI & Video Elements")]
    public GameObject cutscenePanel;      // IntroCutscenePanel RawImage object
    public VideoPlayer videoPlayer;       // Reference to VideoPlayer component
    public VideoClip cutsceneVideoClip;   // Drag your .mp4 asset here in Inspector
    public GameObject mainMenuUI;         // Main menu UI canvas/panel to hide

    [Header("Scene Transition Settings")]
    public string nextLevelName = "Level1_DroneRoom"; // First gameplay level scene name

    private bool isPlayingCutscene = false;

    void Start()
    {
        if (cutscenePanel != null)
        {
            cutscenePanel.SetActive(false);
        }

        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    void Update()
    {
        if (isPlayingCutscene && Input.GetKeyDown(KeyCode.Space))
        {
            SkipCutscene();
        }
    }

    public void StartGameSequence()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopMusic();
        }

        if (mainMenuUI != null)
        {
            mainMenuUI.SetActive(false);
        }

        // Start Coroutine to allow VideoPlayer time to wake up
        StartCoroutine(PrepareAndPlayVideo());
    }

    private IEnumerator PrepareAndPlayVideo()
    {
        if (cutscenePanel != null && videoPlayer != null)
        {
            // 1. Activate the panel
            cutscenePanel.SetActive(true);

            // 2. Wait 1 frame for Unity to register the VideoPlayer component as enabled
            yield return null;

            // 3. Bind clip directly
            if (cutsceneVideoClip != null)
            {
                videoPlayer.clip = cutsceneVideoClip;
            }

            // 4. Hook prepare event and prepare
            videoPlayer.prepareCompleted += OnVideoPrepared;
            videoPlayer.Prepare();
        }
        else
        {
            LoadFirstLevel();
        }
    }

    void OnVideoPrepared(VideoPlayer vp)
    {
        videoPlayer.prepareCompleted -= OnVideoPrepared;
        videoPlayer.Play();
        isPlayingCutscene = true;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        LoadFirstLevel();
    }

    public void SkipCutscene()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
        }
        LoadFirstLevel();
    }

    void LoadFirstLevel()
    {
        isPlayingCutscene = false;
        if (!string.IsNullOrEmpty(nextLevelName))
        {
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            Debug.LogError("Next Level Name is missing in CutsceneController!");
        }
    }
}