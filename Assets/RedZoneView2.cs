using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RedZoneView2 : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public int segments = 20;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;
    private LineRenderer lineRenderer;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private Mesh mesh;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FOVRoutine());

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 4; // Deux lignes + le centre
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.yellow;

        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Standard"));
        mesh = new Mesh();
        meshFilter.mesh = mesh;
        

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
 /*   private void OnDrawGizmos()
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
    }*/
    void DrawFieldOfView()
    {
        Vector3 frontLeft = DirectionFromAngle(transform.eulerAngles.y, -angle / 2) * radius;
        Vector3 frontRight = DirectionFromAngle(transform.eulerAngles.y, angle / 2) * radius;


        //lineRenderer.SetPosition(0,transform.position);
        //lineRenderer.SetPosition(1, transform.position + frontLeft);
        //lineRenderer.SetPosition(2, transform.position + frontRight);
        //lineRenderer.SetPosition(3, transform.position);
        
        int stepCounts = segments + 1;
        float stepAngleSize = angle / (float)segments;
        List<Vector3> vertices = new List<Vector3> { Vector3.zero }; // Point central du cône
        List<int> triangles = new List<int>();
        for (int i = 0; i <= stepCounts; i++)
        {
            float currentAngle = -angle / 2 + stepAngleSize * i;
            Vector3 vertex = DirectionFromAngle(transform.eulerAngles.y, currentAngle) * radius;
            vertices.Add(vertex.normalized * radius * (1 / transform.localScale.x));
        }
        /*for (int i = 1; i < vertices.Count; i++)
        {
            vertices[i] *= radius;
        }*/
        for (int i = 1; i < vertices.Count - 1; i++)
        {
            triangles.Add(0);
            triangles.Add(i);
            triangles.Add(i + 1);
        }
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();

        meshRenderer.material.color = canSeePlayer ? new Color(1f, 0f, 0f, 0.5f) : new Color(1f, 1f, 0f, 0.5f);

        // Dessiner les limites de l'angle du champ de vision (ligne jaune)
        Debug.DrawLine(transform.position, transform.position + frontLeft, Color.yellow,100f);
        Debug.DrawLine(transform.position, transform.position + frontRight, Color.yellow,100f);
        Debug.DrawLine(new Vector3(52,1,58),new Vector3(52,1,100),Color.yellow,100f);
        // Si le joueur est visible, dessiner une ligne rouge
        /*if (canSeePlayer)
        {
            lineRenderer.startColor= Color.red;
            lineRenderer.endColor= Color.red;
        }
        else
        {
            lineRenderer.startColor= Color.yellow;
            lineRenderer.endColor= Color.yellow;
        }*/
    }
    void Update()
    {
        DrawFieldOfView();
    }
}
