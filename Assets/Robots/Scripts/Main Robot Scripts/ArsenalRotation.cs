using UnityEngine;

//This is a modified ArsenalScript for managing Arsenals as they turn to face the player and audio distractions

public class ArsenalRotation : MonoBehaviour
{
    //variables for vision cone
    public float rotateTimer = 0f;
    public bool rotateFlag = false;
    public float fov;
    [Range(0, 360)] public float fovAngle;
    public bool playerSpotted;
    
    //Targetting variables
    public Transform playerTarget;
    public Transform target;
    public Color radiusColor;
    public Quaternion defaultRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defaultRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //incrementing timer
        if (!rotateFlag)
        {
            rotateTimer += 0.012f;
        }
        

        //rotating to given target
        if (target != null && playerSpotted == true)
        {
            rotateFlag = false;
            rotateTimer = 0f;

            Vector3 altDirection = target.position;
            altDirection.y = transform.position.y;

            Vector3 direction = altDirection - transform.position;
            
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5.0f * Time.deltaTime);
        }


        //default rotation cycle
        if (rotateTimer >= 5f)
        {
            //once timer reaches 10 seconds, activate flag
            rotateFlag = true;
            rotateTimer = 0f;
        }
        //if flag is active, move to default
        if (rotateFlag)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, defaultRotation, 5.0f * Time.deltaTime);
        }
        //if done rotating, reset flag
        if (transform.rotation == defaultRotation)
        {
            rotateFlag = false;
        }

        //main system for checking nearby entities
        playerSpotted = false;
        //two lists, one for audio at full range and players at half range
        Collider[] playersNearby = Physics.OverlapSphere(transform.position, fov/2);
        Collider[] soundsNearby = Physics.OverlapSphere(transform.position, fov);

        //Checking for audio distractions
        foreach (Collider c in soundsNearby)
        {
            //if tag is audio lure, AND signal is being sent
            if (c.CompareTag("Lure"))
            {
                if (c.TryGetComponent(out SignalToggle signal))
                {
                    if (signal.signalActive == true)
                    {
                        target = c.transform;

                        float signedAngle;
                        signedAngle = Vector3.Angle(
                        transform.forward,
                        c.transform.position - transform.position);

                        if (Mathf.Abs(signedAngle) < fovAngle / 2)
                        {
                            playerSpotted = true;
                            rotateTimer = 0f;
                        }
                        break;
                    }
                }
            }
        }

        //checking nearby players
        foreach (Collider c in playersNearby)
        {
            if (c.CompareTag("Player"))
            {
                float signedAngle;
                signedAngle = Vector3.Angle(
                transform.forward,
                c.transform.position - transform.position);

                if (Mathf.Abs(signedAngle) < fovAngle / 2)
                {
                    playerSpotted = true;
                    target = playerTarget;
                    rotateTimer = 0f;
                }
                break;
            }
        }
    }
}
