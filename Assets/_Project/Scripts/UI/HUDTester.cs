using UnityEngine;

public class HUDTester : MonoBehaviour
{
    [SerializeField] private string newObjective = "Find the Door Keycard";
    [SerializeField] private string popupTitle = "Keycard Picked Up";
    [SerializeField] private string popupDescription = "Use this keycard to access the next room.";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (HUDController.Instance != null)
            {
                // Update the top-left objective text
                HUDController.Instance.UpdateObjective(newObjective);

                // Display the center description popup for 4 seconds
                HUDController.Instance.ShowDescriptionPopup(popupTitle, popupDescription, 4f);
            }
        }
    }
}