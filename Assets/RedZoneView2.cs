using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedZoneView2 : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }
    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                }
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }
    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radius);

        Vector3 viewAngle01 = DirectionFromAngle(transform.eulerAngles.y, -angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(transform.eulerAngles.y, angle / 2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + viewAngle01 * radius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngle02 * radius);

        if(canSeePlayer)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, playerRef.transform.position);
        }
    }
    void DrawFieldOfView()
    {
        Vector3 frontLeft = DirectionFromAngle(transform.eulerAngles.y, -angle / 2) * radius;
        Vector3 frontRight = DirectionFromAngle(transform.eulerAngles.y, angle / 2) * radius;

        // Dessiner les limites de l'angle du champ de vision (ligne jaune)
        Debug.DrawLine(transform.position, transform.position + frontLeft, Color.yellow);
        Debug.DrawLine(transform.position, transform.position + frontRight, Color.yellow);

        // Si le joueur est visible, dessiner une ligne rouge
        if (canSeePlayer)
        {
            Debug.DrawLine(transform.position, playerRef.transform.position, Color.red);
        }
    }
    void Update()
    {
        DrawFieldOfView();
    }
}
