using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(ArsenalScript))]
public class ArsenalBehaviorEditor : Editor
{
    private void OnSceneGUI()
    {
        ArsenalScript _arsenal = (ArsenalScript)target;

        Color c = Color.green;
        
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
            _arsenal.transform.position,
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
