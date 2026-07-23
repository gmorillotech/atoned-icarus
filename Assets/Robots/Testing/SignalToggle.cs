using UnityEngine;

public class SignalToggle : MonoBehaviour
{
    public bool signalActive;
    public Material color;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        signalActive = false;
        color.DisableKeyword("_EMISSION");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (signalActive)
            {
                signalActive = false;
                color.DisableKeyword("_EMISSION");
            }
            else
            {
                signalActive = true;
                color.EnableKeyword("_EMISSION");

            }
        }
    }
}
