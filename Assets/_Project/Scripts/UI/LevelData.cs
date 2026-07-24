using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "Game/Level Data")]
public class LevelData : ScriptableObject
{
    public string levelName;
    public string speakerName = "Icarus";
    [TextArea(3, 5)]
    public string tutorialMessage;
    public Sprite speakerPortrait;
}