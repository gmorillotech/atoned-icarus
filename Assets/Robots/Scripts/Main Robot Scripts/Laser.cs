using UnityEngine;
using UnityEngine.Events;

public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float laserDistance = 4f;
    [SerializeField] private LayerMask ignoreMask;
    [SerializeField] private UnityEvent OnHitTarget;

    private RaycastHit rayHit;
    private Ray ray;
    private bool hasHit;
    
    public ArsenalScript mainScript;

    private void Start()
    {
        lineRenderer.positionCount = 2;
    }

    private void Update()
    {
        ray = new Ray(transform.position, transform.forward);

        hasHit = Physics.Raycast(ray, out rayHit, laserDistance, ~ignoreMask);

        if (hasHit)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, rayHit.point);

            if (rayHit.collider.CompareTag("Player"))
            {
                mainScript.playerShot = true;
            }

        }
        else
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + transform.forward * laserDistance);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray.origin, ray.direction * laserDistance);

        if (hasHit)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(rayHit.point, 0.23f);
        }
    }
}
