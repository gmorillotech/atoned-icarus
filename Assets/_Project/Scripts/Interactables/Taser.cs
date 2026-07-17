using UnityEngine;

public class Taser : MonoBehaviour
{
    [SerializeField] private float stunRange = 5f;
    [SerializeField] private float stunAngle = 45f;
    [SerializeField] private LayerMask enemyLayer;

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
            enemyLayer
        );

        foreach (Collider obj in objectsInRange)
        {

            if (obj.transform == playerTransform)
                continue;

            Vector3 directionToTarget =
                (obj.transform.position - playerTransform.position).normalized;

            float angle = Vector3.Angle(
                playerTransform.forward,
                directionToTarget
            );

            if (angle <= stunAngle)
            {
                Debug.Log("Stunned: " + obj.name);
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