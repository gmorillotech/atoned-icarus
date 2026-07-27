using UnityEngine;
using TMPro;

public class LevelObjective : MonoBehaviour
{
    [Header("UI Component References")]
    [SerializeField] private TMP_Text objectiveText;

    [Header("Current Level Data")]
    [SerializeField] private LevelData currentLevelData;

    private void Start()
    {
        UpdateObjectiveDisplay();
    }

    public void UpdateObjectiveDisplay()
    {
        if (currentLevelData != null && objectiveText != null)
        {
            objectiveText.text = currentLevelData.levelObjective;
        }
    }

    // Call this dynamically if the player completes an objective mid-level!
    public void SetNewObjective(string newObjective)
    {
        if (objectiveText != null)
        {
            objectiveText.text = newObjective;
        }
    }
}