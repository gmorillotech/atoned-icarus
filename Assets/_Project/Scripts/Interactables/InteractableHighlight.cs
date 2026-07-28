using UnityEngine;

public class InteractableHighlight : MonoBehaviour
{
    [Header("Highlight Settings")]
    [SerializeField] private Light highlightLight;
    [SerializeField] private float activationDistance = 12f;
    [SerializeField] private float maxIntensity = 110f;
    [SerializeField] private float pulseSpeed = 1.5f;
    [SerializeField] private float minPulseIntensity = 40f;

    private Transform player;
    private float originalIntensity;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (highlightLight != null)
        {
            originalIntensity = highlightLight.intensity;
            highlightLight.intensity = 0f;
        }
    }

    private void Update()
    {
        if (player == null || highlightLight == null)
            return;

        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        if (distance <= activationDistance)
        {
            float strength = 1f - (distance / activationDistance);

            float pulse = Mathf.Lerp(
                minPulseIntensity,
                maxIntensity,
                (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f
            );

            highlightLight.intensity = Mathf.Lerp(
                highlightLight.intensity,
                pulse * strength,
                Time.deltaTime * 5f
            );
        }
        else
        {
            highlightLight.intensity = Mathf.Lerp(
                highlightLight.intensity,
                0f,
                Time.deltaTime * 5f
            );
        }
    }
}