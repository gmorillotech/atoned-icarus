using UnityEngine;

public class SlipperySurface : MonoBehaviour
{
    [SerializeField] private float slideSpeed = 8f;

    private void OnCollisionStay(Collision collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            ContactPoint contact = collision.contacts[0];

            // Calculate downhill direction based on the beam angle
            Vector3 slideDirection = Vector3.ProjectOnPlane(
                Vector3.down,
                contact.normal
            ).normalized;

            player.SetSliding(true, slideSpeed, slideDirection);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.SetSliding(false, 0f, Vector3.zero);
        }
    }
}