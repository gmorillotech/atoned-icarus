using UnityEngine;

public class Taser : MonoBehaviour
{
    [SerializeField] private float stunRange = 5f;
    [SerializeField] private float stunAngle = 90f;
    [SerializeField] private LayerMask droneLayer;
    [SerializeField] private LineRenderer taserBeam;
    [SerializeField] private AudioSource taserAudio;

    private Transform playerTransform;

    private void Awake()
    {
        playerTransform = FindFirstObjectByType<PlayerController>().transform;
    }

    public bool Activate()
    {
        bool droneHit = false;

        Debug.DrawRay(
            playerTransform.position,
            playerTransform.forward * stunRange,
            Color.red,
            5f
        );

        Collider[] objectsInRange = Physics.OverlapSphere(
            playerTransform.position,
            stunRange,
            droneLayer
        );

        foreach (Collider obj in objectsInRange)
        {

            Debug.Log("Found collider: " + obj.name);

            if (obj.transform == playerTransform)
                continue;

            // Ignore height difference when checking the stun cone
            Vector3 directionToTarget = obj.transform.position - playerTransform.position;
            directionToTarget.y = 0f;
            directionToTarget.Normalize();

            // Ignore the player's vertical look direction too
            Vector3 playerForward = playerTransform.forward;
            playerForward.y = 0f;
            playerForward.Normalize();

            float angle = Vector3.Angle(playerForward, directionToTarget);

            if (angle <= stunAngle)
            {
                DroneController drone = obj.GetComponent<DroneController>();

                if (drone != null)
                {
                    drone.stunned = true;
                    droneHit = true;
                    ShowBeam(obj.transform.position);

                    Debug.Log("Drone stunned!");
                }
            }
        }

        if(droneHit)
        {
            taserAudio.pitch = Random.Range(0.9f, 1.1f);
            taserAudio.Play();
        }

        return droneHit;
    }

    private void OnDrawGizmosSelected()
    {
        if (playerTransform == null)
            return;

        Gizmos.color = Color.yellow;

        Vector3 leftBoundary =
            Quaternion.Euler(0, -stunAngle, 0) * playerTransform.forward;

        Vector3 rightBoundary =
            Quaternion.Euler(0, stunAngle, 0) * playerTransform.forward;

        Gizmos.DrawRay(
            playerTransform.position,
            leftBoundary * stunRange
        );

        Gizmos.DrawRay(
            playerTransform.position,
            rightBoundary * stunRange
        );

        Gizmos.DrawRay(
            playerTransform.position,
            playerTransform.forward * stunRange
        );
    }

    private void ShowBeam(Vector3 targetPosition)
    {
        taserBeam.enabled = true;

        int points = 8;
        taserBeam.positionCount = points;

        Vector3 start = playerTransform.position;
        Vector3 direction = targetPosition - start;

        for (int i = 0; i < points; i++)
        {
            float t = i / (float)(points - 1);

            Vector3 point = Vector3.Lerp(start, targetPosition, t);

            // Add random electricity movement
            if (i != 0 && i != points - 1)
            {
                point += new Vector3(
                    Random.Range(-0.2f, 0.2f),
                    Random.Range(-0.2f, 0.2f),
                    Random.Range(-0.2f, 0.2f)
                );
            }

            taserBeam.SetPosition(i, point);
        }

        Invoke(nameof(HideBeam), 0.1f);
    }

    private void HideBeam()
    {
        taserBeam.enabled = false;
    }
}