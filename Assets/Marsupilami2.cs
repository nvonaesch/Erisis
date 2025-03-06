using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marsupilami2 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject buzz;
    public GameObject center;
    public float radius;
    [Range(0, 360)]
    public float angle;

    public float Speed = 2f;
    public int segments = 20;
    
    public GameObject playerRef;

    public bool canSeePlayer;
    public bool hasRotated = false;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private Mesh mesh;
    void Start()
    {
        StartCoroutine(FOVRoutine());

        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Standard"));
        mesh = new Mesh();
        meshFilter.mesh = mesh;
        DrawFieldOfView();
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
                    DrawFieldOfView();
                }
                else
                    canSeePlayer = false;
                    DrawFieldOfView();
            }
            else
                canSeePlayer = false;
                DrawFieldOfView();
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

    void DrawFieldOfView()
    {
        Vector3 frontLeft = DirectionFromAngle(transform.eulerAngles.y, -angle / 2) * radius;
        Vector3 frontRight = DirectionFromAngle(transform.eulerAngles.y, angle / 2) * radius;

        
        int stepCounts = segments + 1;
        float stepAngleSize = angle / (float)segments;
        List<Vector3> vertices = new List<Vector3> { Vector3.up * 2.5f }; // Point central du cône
        List<int> triangles = new List<int>();
        for (int i = 0; i <= stepCounts; i++)
        {
            float currentAngle = -angle / 2 + stepAngleSize * i;
            Vector3 vertex = DirectionFromAngle(transform.eulerAngles.y, currentAngle) * radius + (Vector3.up * 0.7f);
            vertices.Add(vertex.normalized * radius * (1 / transform.localScale.x));
        }
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

        if (canSeePlayer)
        {
            meshRenderer.material.color = Color.red;
        }
        else
        {
            meshRenderer.material.color = Color.yellow;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        buzz.transform.Translate(Vector3.forward*Time.deltaTime * Speed);
        if (!hasRotated)
        {
            if (center.transform.position.z > 80)
            {
                StartCoroutine(demiTourRoutine());
                Debug.Log("jsuis sorti 1 ");
            }
            else if (center.transform.position.z <= 65 )
            {
                StartCoroutine(demiTourRoutine());
                Debug.Log("jsuis sorti 2 ");
            }
        }
        //DrawFieldOfView();
        
    }
    private IEnumerator demiTourRoutine()
    {
        hasRotated = true;
        WaitForSeconds wait = new WaitForSeconds(1f);
        Debug.Log("frr jsuis dedans");
        yield return wait;
        buzz.transform.rotation *= Quaternion.Euler(0f, 180, 0f);
        yield return wait;
        hasRotated = false;
        //yield break;
        
    }
    /*private void OnTriggerEnter(Collider other)
    {
        if(other = "Buzzrotate")
        {
            buzz.transform.rotation *= Quaternion.Euler(0f, 180, 0f);
        }
    }*/
}
