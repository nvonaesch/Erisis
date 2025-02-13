using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RedZoneView2))]

public class RedZoneView2Editor : Editor
{
    void OnSceneGUI()
    {
        RedZoneView2 rzv = (RedZoneView2)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(rzv.transform.position, Vector3.up, Vector3.forward, 360, rzv.radius);

        Vector3 viewAngle01 = DirectionFromAngle(rzv.transform.eulerAngles.y, -rzv.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(rzv.transform.eulerAngles.y, rzv.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(rzv.transform.position, rzv.transform.position + viewAngle01 * rzv.radius);
        Handles.DrawLine(rzv.transform.position, rzv.transform.position + viewAngle02 * rzv.radius);

        if (rzv.canSeePlayer)
        {
            Handles.color = Color.red;
            Handles.DrawLine(rzv.transform.position, rzv.playerRef.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
