using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float BASE_SPEED = 7f;
    [SerializeField] private float JUMP_FORCE = 8f;
    [SerializeField] private float rotationSpeed = 12f;
    
    private Rigidbody rb;
    private float currentSpeed;
    private Vector3 moveInput;

    public enum MovementMode { TopDown, SideScroller }
    [Header("Current State")]
    public MovementMode currentMode;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = BASE_SPEED;
        DetermineMovementMode();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        if (currentMode == MovementMode.TopDown)
        {
            moveInput = new Vector3(moveX, 0f, moveZ).normalized;

            // FIX: Smoothly rotate the character without breaking physics constraints
            if (moveInput != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveInput);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else if (currentMode == MovementMode.SideScroller)
        {
            moveInput = new Vector3(moveX, 0f, 0f).normalized;

            // Smoothly turn left or right along the 2D plane
            if (moveX > 0) transform.rotation = Quaternion.Euler(0, 90, 0);
            else if (moveX < 0) transform.rotation = Quaternion.Euler(0, -90, 0);

            // Jump handling
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, JUMP_FORCE, 0f);
            }
        }
    }

    void FixedUpdate()
    {
        if (currentMode == MovementMode.TopDown)
        {
            rb.linearVelocity = new Vector3(moveInput.x * currentSpeed, rb.linearVelocity.y, moveInput.z * currentSpeed);
        }
        else if (currentMode == MovementMode.SideScroller)
        {
            rb.linearVelocity = new Vector3(moveInput.x * currentSpeed, rb.linearVelocity.y, 0f);
        }
    }

    public void DetermineMovementMode()
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.ToLower();

        if (sceneName.Contains("side") || sceneName.Contains("scroller") || sceneName.Contains("vent") || sceneName.Contains("shaft"))
        {
            currentMode = MovementMode.SideScroller;
            rb.useGravity = true;
            // HARD FREEZE all physics physics tracking except Y for gravity and X for running
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
        }
        else
        {
            currentMode = MovementMode.TopDown;
            rb.useGravity = false;
            // HARD FREEZE physics engine rotation entirely
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        }
    }

    private bool IsGrounded()
    {
        // Get the main collider on this object (BoxCollider or CapsuleCollider)
        Collider myCollider = GetComponent<Collider>();
        
        Vector3 checkPosition;
        Vector3 boxHalfExtents;

        if (myCollider != null)
        {
            // Center the box exactly at the bottom boundary of the collider
            float colliderBottomY = myCollider.bounds.center.y - myCollider.bounds.extents.y;
            checkPosition = new Vector3(transform.position.x, colliderBottomY, transform.position.z);
            
            // Size the box width relative to the model size, keeping it thin vertically
            boxHalfExtents = new Vector3(myCollider.bounds.extents.x * 0.8f, 0.1f, myCollider.bounds.extents.z * 0.8f);
        }
        else
        {
            // Fallback default math if no collider is detected on the root object
            checkPosition = transform.position + (Vector3.down * 1.0f);
            boxHalfExtents = new Vector3(0.3f, 0.1f, 0.3f);
        }

        // Exclude the Player layer completely
        int layerMask = ~LayerMask.GetMask("Player"); 

        Collider[] colliders = Physics.OverlapBox(checkPosition, boxHalfExtents, Quaternion.identity, layerMask);

        foreach (var col in colliders)
        {
            // Ensure we are hitting a solid floor, not a trigger zone
            if (!col.isTrigger)
            {
                return true; 
            }
        }
        return false;
    }
}