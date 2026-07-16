using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Controller : MonoBehaviour
{
    public List<Transform> wayPoint;
    public NavMeshAgent agent;
    public DigitalRuby.LightningBolt.LightningBoltScript lightning;

    public int waypointIndex = 0;
    public float timer = 0;
    public float timerLength = 5;
    public bool stunned = false;
    public bool foundPlayer = false;
    public int checkpoint = 0;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //stunned by taser, LEFT BUTTON
        if (Input.GetMouseButtonDown(0))
        {
            stunned = true;
        }

        //player caught, RIGHT BUTTON
        if (Input.GetMouseButtonDown(1))
        {
            stunned = true;
            foundPlayer = true;
        }

        if (!stunned)
        {
            //is not stunned, continue movement
            Patrol();
        }
        else if (timer >= timerLength)
        {
            //if in the middle of being stunned
            timer = 0f;
            stunned = false;
            agent.speed = 3f;
            foundPlayer = false;
        }
        else
        {
            //if the timer has run out, no longer stunned
            agent.speed = 0f;
            timer = (timer + 0.015f);
            if ((foundPlayer == true) && timer < (timerLength-2))
            {
                lightning.Trigger();
            }
        }
    }

    void Patrol()
    {
        //ensuring there's at least one spot to move to
        if (wayPoint.Count == 0)
        {
            return;
        }

        //measuring distance between object and destination
        float distance = Vector3.Distance(wayPoint[waypointIndex].position, transform.position);

        //if destination is close, switch to the next destination
        if (distance <= 0.5)
        {
            waypointIndex = (waypointIndex + 1) % wayPoint.Count;
        }

        agent.SetDestination(wayPoint[waypointIndex].position);
    }
}
