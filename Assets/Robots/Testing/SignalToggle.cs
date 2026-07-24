using UnityEngine;

public class SignalToggle : MonoBehaviour
{
    public bool signalActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        signalActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            signalActive = !signalActive;
        }
    }
}
