using UnityEngine;

public class Taser : MonoBehaviour
{
    [SerializeField] private float stunRange = 5f;

    public void Activate()
    {
        Debug.Log("⚡ TASER ACTIVATED!");

        Collider[] objectsInRange = Physics.OverlapSphere(
            transform.position,
            stunRange
        );

        foreach (Collider obj in objectsInRange)
        {
            Debug.Log("Hit: " + obj.name);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, stunRange);
    }
}