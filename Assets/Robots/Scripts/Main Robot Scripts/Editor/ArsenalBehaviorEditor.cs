using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

//This script is for managing the development vision cone for BOTH Arsenal and Drone
//It's listed as Arsenal but was modified for use with Drones as well

[CustomEditor(typeof(ArsenalScript))]
public class ArsenalBehaviorEditor : Editor
{
    private void OnSceneGUI()
    {
        ArsenalScript _arsenal = (ArsenalScript)target;

        //offsetting center for arsenal
        Vector3 altPosition = _arsenal.transform.position;
        altPosition.x += 0.5f;

        Color c = Color.green;
        
        //Adjusting field color. Not necessary
        if (_arsenal.alertStage == AlertStage.Alert)
        {
            c = Color.Lerp(Color.green, Color.red, _arsenal.alertLevel / 100f);
        }
        else if (_arsenal.alertStage == AlertStage.Attack)
        {
            c = Color.red;
        }

        Handles.color = new Color(c.r, c.g, c.b, 0.3f);
        
        //Arsenal vision
        if (_arsenal.robotType == RobotType.Arsenal)
        {
            Handles.DrawSolidArc(
            altPosition,
            _arsenal.transform.up,
            Quaternion.AngleAxis(-_arsenal.fovAngle / 2f, _arsenal.transform.up) * _arsenal.transform.forward,
            _arsenal.fovAngle,
            _arsenal.fov);
        }

        //Drone vision
        else if (_arsenal.robotType == RobotType.Drone)
        {
            //Vector3 spot = (_arsenal.transform.position.x, _arsenal.transform.position.y + 3f, _arsenal.transform.position.z);
            Vector3 spot = _arsenal.transform.position;
            spot.y += 2f;
            Handles.DrawSolidArc(
                spot,
                _arsenal.transform.forward,
                Quaternion.AngleAxis(-_arsenal.fovAngle / 2f, _arsenal.transform.forward) * -_arsenal.transform.up,
                _arsenal.fovAngle,
                _arsenal.fov);
        }

        Handles.color = c;
        _arsenal.fov = Handles.ScaleValueHandle(
            _arsenal.fov,
            _arsenal.transform.position+_arsenal.transform.forward * _arsenal.fov,
            _arsenal.transform.rotation,
            3,
            Handles.SphereHandleCap,
            1);
    }
}
