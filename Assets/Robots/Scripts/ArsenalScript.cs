using UnityEngine;

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
    public float fov;
    [Range(0, 360)] public float fovAngle;
    public bool playerSpotted;
    public RobotType robotType;
    public AlertStage alertStage;
    [Range(0, 100)] public float alertLevel; //0 idle, 100 attack

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
                /*
            case AlertStage.Alert:
                if (playerSpotted)
                {
                    alertLevel++;
                    if (alertLevel >= 100)
                    {
                        alertStage = AlertStage.Attack;
                    }
                }
                else
                {
                    alertLevel--;
                    if (alertLevel <= 0)
                    {
                        alertStage = AlertStage.Idle;
                    }
                }
                break;*/
            case AlertStage.Attack:
                /*
                if (robotType == RobotType.Drone)
                {

                }*/

                if (!playerSpotted)
                {
                    alertStage = AlertStage.Idle;
                }
                break;
        }
    }
}
