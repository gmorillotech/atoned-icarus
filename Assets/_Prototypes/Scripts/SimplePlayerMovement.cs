using UnityEngine;

public class SimplePlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody rb;
    private Vector3 moveDirection;

    void Start()
    {
        // Grabs the Rigidbody from your player capsule
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Reset direction every frame
        moveDirection = Vector3.zero;

        // ⌨️ Your exact manual keyboard checks!
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveDirection += Vector3.forward; // Move up/forward
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveDirection += Vector3.back; // Move down/back
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection += Vector3.left; // Move left
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection += Vector3.right; // Move right
        }

        // Keep diagonal movement speed consistent
        moveDirection = moveDirection.normalized;
    }

    void FixedUpdate()
    {
        // This is what pushes your player through physics, making walls solid!
        Vector3 targetPosition = rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(targetPosition);
    }
}
