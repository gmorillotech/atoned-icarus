using UnityEngine;

public class Taser : MonoBehaviour
{
    [SerializeField] private float stunRange = 5f;
    [SerializeField] private float stunAngle = 90f;
    [SerializeField] private LayerMask droneLayer;

    private Transform playerTransform;

    private void Awake()
    {
        playerTransform = FindFirstObjectByType<PlayerController>().transform;
    }

    public void Activate()
    {

        Debug.Log("⚡ TASER ACTIVATED!");

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
                    Debug.Log("Drone stunned!");
                }
            }
        }
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
}