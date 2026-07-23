using UnityEngine;

//This is the general script for Arsenals, that was also modified for use with Drones
//mainly focuses on cone vision and detecting the player

public enum AlertStage
{
    Idle,
    Alert,
    Attack
}

public enum RobotType
{
    Arsenal,
    Drone
}

public class ArsenalScript : MonoBehaviour
{
    public bool playerShot = false;
    public float fov;
    [Range(0, 360)] public float fovAngle;
    public bool playerSpotted;
    public RobotType robotType;
    public AlertStage alertStage;
    [Range(0, 100)] public float alertLevel; //0 idle, 100 attack
    public Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //robotType = robotType.Drone;
        alertStage = AlertStage.Idle;
        alertLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        playerSpotted = false;
        Collider[] targetsSpotted = Physics.OverlapSphere(
            transform.position, fov);
        foreach (Collider c in targetsSpotted)
        {
            if (c.CompareTag("Player"))
            {
                float signedAngle;
                if (robotType == RobotType.Arsenal)
                {
                    signedAngle = Vector3.Angle(
                    transform.forward,
                    c.transform.position - transform.position);
                }
                else
                {
                    signedAngle = Vector3.Angle(
                    -transform.up,
                    c.transform.position - transform.position);
                }

                if (Mathf.Abs(signedAngle) < fovAngle / 2)
                {
                    playerSpotted = true;
                }
                break;
            }
        }
        UpdateAlertState(playerSpotted);
    }

    private void UpdateAlertState(bool playerSpotted)
    {
        switch (alertStage)
        {
            case AlertStage.Idle:
                if (playerSpotted)
                {
                    alertStage = AlertStage.Attack;
                }
                break;
            case AlertStage.Attack:
                if (!playerSpotted)
                {
                    alertStage = AlertStage.Idle;
                }
                break;
        }
    }
}
