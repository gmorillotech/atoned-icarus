using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float BASE_SPEED = 4f;
    [SerializeField] private float JUMP_FORCE = 8f;
    [SerializeField] private float SNEAK_SPEED = 2f;
    [SerializeField] private float rotationSpeed = 12f;
    
    private PlayerHealth playerHealth;
    private Rigidbody rb;
    private float currentSpeed;
    private Animator animator;
    private Vector3 moveInput;

    private bool isSneaking;
    private Vector3 facingDirection;

    public bool IsSneaking => isSneaking;
    public Vector3 FacingDirection => facingDirection;

    public enum MovementMode { TopDown, SideScroller }
    [Header("Current State")]
    public MovementMode currentMode;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();

        currentSpeed = BASE_SPEED;
        
        // Initial setup on start
        var initialConfig = Object.FindFirstObjectByType<SceneConfiguration>();
        DetermineMovementMode(initialConfig);
    }

    void Update()
    {

        if (playerHealth != null && playerHealth.IsDead) return;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        isSneaking = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        currentSpeed = isSneaking ? SNEAK_SPEED : BASE_SPEED;

        bool isMoving = rb.linearVelocity.magnitude > 0.1f;

        if (animator != null)
        {
            animator.SetBool("IsSneaking", isSneaking && isMoving);

            float animationSpeed = isMoving ? 1f : 0f;
            animator.SetFloat("Speed", animationSpeed);
        }

        if (currentMode == MovementMode.TopDown)
        {
            moveInput = new Vector3(moveX, 0f, moveZ).normalized;

            // Smoothly rotate the character without breaking physics constraints
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
        if (playerHealth != null && playerHealth.IsDead) return;

        if (currentMode == MovementMode.TopDown)
        {
            rb.linearVelocity = new Vector3(moveInput.x * currentSpeed, rb.linearVelocity.y, moveInput.z * currentSpeed);
        }
        else if (currentMode == MovementMode.SideScroller)
        {
            rb.linearVelocity = new Vector3(moveInput.x * currentSpeed, rb.linearVelocity.y, 0f);
        }
    }

    // --- TRIGGER DETECTION FOR HAZARDS ---
    private void OnTriggerEnter(Collider other)
    {
        // If the player touches a hazard tagged "KillZone"
        if (other.CompareTag("KillZone"))
        {
            Debug.Log("[Player] Entered a KillZone!");
            DieAndRespawn();
        }
    }

    // --- RESPAWN METHOD ---
    public void DieAndRespawn()
    {
        if (CheckpointManager.Instance != null)
        {
            // Reset Rigidbody momentum so you don't carry falling speed into the respawn
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            CheckpointManager.Instance.LoadLastCheckpoint(gameObject);
        }
        else
        {
            Debug.LogWarning("[Player] No CheckpointManager found in the scene to handle respawn!");
        }
    }

    public void DetermineMovementMode(SceneConfiguration sceneConfig)
    {
        // Make sure rb is cached even if this runs early
        if (rb == null) rb = GetComponent<Rigidbody>();

        if (sceneConfig != null)
        {
            if (sceneConfig.LevelType == LevelType.SideScroller)
            {
                currentMode = MovementMode.SideScroller;
                
                if (rb != null)
                {
                    rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                    
                    // George's Requirement: Explicitly set gravity to true for Side-Scroll
                    rb.useGravity = true; 
                }
                Debug.Log($"[PlayerController] Successfully switched to SideScroller movement mode.");
            }
            else
            {
                currentMode = MovementMode.TopDown;

                if (rb != null)
                {
                    rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY; 
                    
                    // George's Requirement: Turn off gravity and zero out vertical velocity for Top-Down
                    rb.useGravity = false;
                    rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
                }
                Debug.Log($"[PlayerController] Successfully switched to Top-Down movement mode.");
            }
        }
        else
        {
            Debug.LogWarning("[PlayerController] DetermineMovementMode received a null SceneConfiguration! Defaulting to Top-Down.");
            currentMode = MovementMode.TopDown;
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                rb.useGravity = false;
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            }
        }
    }

    private bool IsGrounded()
    {
        Collider myCollider = GetComponent<Collider>();
        Vector3 checkPosition;
        Vector3 boxHalfExtents;

        if (myCollider != null)
        {
            float colliderBottomY = myCollider.bounds.center.y - myCollider.bounds.extents.y;
            checkPosition = new Vector3(transform.position.x, colliderBottomY, transform.position.z);
            boxHalfExtents = new Vector3(myCollider.bounds.extents.x * 0.8f, 0.1f, myCollider.bounds.extents.z * 0.8f);
        }
        else
        {
            checkPosition = transform.position + (Vector3.down * 1.0f);
            boxHalfExtents = new Vector3(0.3f, 0.1f, 0.3f);
        }

        int layerMask = ~LayerMask.GetMask("Player"); 
        Collider[] colliders = Physics.OverlapBox(checkPosition, boxHalfExtents, Quaternion.identity, layerMask);

        foreach (var col in colliders)
        {
            if (!col.isTrigger) return true; 
        }
        return false;
    }
}