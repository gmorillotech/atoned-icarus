using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Taser currentTaser;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentTaser != null)
            {
                currentTaser.Activate();
            }
        }
    }

    public void PickupTaser(Taser taser)
    {
        currentTaser = taser;
        Debug.Log("Taser stored!");
    }
}