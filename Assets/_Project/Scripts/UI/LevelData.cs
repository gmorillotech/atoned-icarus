using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "Game/Level Data")]
public class LevelData : ScriptableObject
{
    public string levelName;
    
    [Header("Dialogue Settings")]
    public string speakerName = "Icarus";
    [TextArea(3, 5)]
    public string tutorialMessage;
    public Sprite speakerPortrait;
    
    // ADD THIS FIELD: Holds the audio clip for this level's dialogue
    public AudioClip dialogueAudioClip;

    [Header("Objective Settings")]
    [TextArea(2, 4)]
    public string levelObjective = "Locate and access the terminal.";
}