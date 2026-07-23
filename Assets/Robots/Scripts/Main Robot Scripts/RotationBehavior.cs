using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(ArsenalRotation))]
public class RotationBehavior : Editor
{
    private void OnSceneGUI()
    {

        ArsenalRotation _arsenal = (ArsenalRotation)target;

        Color c = _arsenal.radiusColor;

        Handles.color = new Color(c.r, c.g, c.b, 0.3f);

        Vector3 altPosition = _arsenal.transform.position;
        //altPosition.z -= 0.23f;
        //altPosition.x += 0.45f;

        //Arsenal vision
            Handles.DrawSolidArc(
            //_arsenal.transform.position,
            altPosition,
            _arsenal.transform.up,
            Quaternion.AngleAxis(-_arsenal.fovAngle / 2f, _arsenal.transform.up) * _arsenal.transform.forward,
            _arsenal.fovAngle,
            _arsenal.fov);


        Handles.color = c;
        _arsenal.fov = Handles.ScaleValueHandle(
            _arsenal.fov,
            _arsenal.transform.position + _arsenal.transform.forward * _arsenal.fov,
            _arsenal.transform.rotation,
            3,
            Handles.SphereHandleCap,
            1);
    }
}
