using System.Collections.Generic;
using UnityEngine;

public class SlipperySurface : MonoBehaviour
{
    [SerializeField] private float slideSpeed = 8f;
    [SerializeField] private float minSlideAngle = 15f;

    // Tracks, per player collider, which slippery surface colliders it is currently touching.
    // Sliding is only cleared once this set is empty, so losing contact with one beam while
    // still touching an adjacent beam doesn't cause a one-frame stutter.
    private static readonly Dictionary<Collider, HashSet<Collider>> activeContacts = new Dictionary<Collider, HashSet<Collider>>();

    private Collider ownCollider;

    private void Awake()
    {
        ownCollider = GetComponent<Collider>();
    }

    private void OnCollisionStay(Collision collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player == null)
        {
            return;
        }

        ContactPoint contact = collision.contacts[0];

        if (Vector3.Angle(Vector3.up, contact.normal) <= minSlideAngle)
        {
            return;
        }

        Collider playerCollider = collision.collider;

        if (!activeContacts.TryGetValue(playerCollider, out HashSet<Collider> contactedSurfaces))
        {
            contactedSurfaces = new HashSet<Collider>();
            activeContacts[playerCollider] = contactedSurfaces;
        }

        contactedSurfaces.Add(ownCollider);

        // Calculate downhill direction based on the beam angle
        Vector3 slideDirection = Vector3.ProjectOnPlane(
            Vector3.down,
            contact.normal
        ).normalized;

        player.SetSliding(true, slideSpeed, slideDirection);
    }

    private void OnCollisionExit(Collision collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player == null)
        {
            return;
        }

        Collider playerCollider = collision.collider;

        if (activeContacts.TryGetValue(playerCollider, out HashSet<Collider> contactedSurfaces))
        {
            contactedSurfaces.Remove(ownCollider);

            if (contactedSurfaces.Count == 0)
            {
                activeContacts.Remove(playerCollider);
                player.SetSliding(false, 0f, Vector3.zero);
            }
        }
    }
}