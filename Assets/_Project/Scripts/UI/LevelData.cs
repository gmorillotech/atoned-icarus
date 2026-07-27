using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "ScriptableObjects/LevelData")]
public class LevelData : ScriptableObject
{
    [Header("Speaker Information")]
    public string speakerName;
    [TextArea(3, 5)] public string tutorialMessage;
    public Sprite speakerPortrait;
    public AudioClip dialogueAudioClip;

    [Header("Display Settings")]
    [Tooltip("How many seconds this dialogue stays on screen. If left at 0, it will automatically match the length of the audio clip!")]
    public float displayDuration = 0f; 

    [Header("Objective Information")]
    public string levelObjective;
}