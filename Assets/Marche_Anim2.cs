using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Marche_Anim2 : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator Titi;
    private string Titilancement = "Walk";
    public GameObject pied;
    public GameObject buzz;
    public GameObject center;
    public float radius;
    [Range(0, 360)]
    public float angle;

    public float power = 500f;
    public float Speed = 2f;
    public int segments = 20;

    private bool dessinDebut = false;
    public GameObject playerRef;

    [Range(0, 107)]
    public float alert;

    public bool canSeePlayer;
    public bool hasRotated = false;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private Mesh mesh;
    public GameObject targetObject1;
    public GameObject targetObject2;
    bool hasStartedBagarre = false;
    public event Action OnAlertMax;

    private bool alertAtMaxAlreadySent = false;
    // Start is called before the first frame update
    void Start()
    {
        Titi = GetComponentInChildren<Animator>();
        StartCoroutine(FOVRoutine());

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
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        meshRenderer.GetPropertyBlock(mpb);
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
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
        if (canSeePlayer && alert < 107)
        {
            alert += 8;
        }
        if (!canSeePlayer && alert > 0)
        {
            alert--;
        }

        if (alert >= 100)
        {
            mpb.SetColor("_Color", Color.red);
            meshRenderer.SetPropertyBlock(mpb);
        }
        if (50 <= alert && alert < 100)
        {
            Color orange = new Color(1.0f, 0.5f, 0.0f);
            mpb.SetColor("_Color", orange);
            meshRenderer.SetPropertyBlock(mpb);
        }
        else if (alert < 50)
        {
            mpb.SetColor("_Color", Color.yellow);
            meshRenderer.SetPropertyBlock(mpb);
        }

    }
    private Vector3 DirectionFromAngle(float angleInDegrees)
    {
        return Quaternion.Euler(0, angleInDegrees, 0) * transform.forward;
        //return Quaternion.AngleAxis(angleInDegrees, Vector3.up) * transform.forward;
    }



    //private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    //{
    //angleInDegrees += eulerY;
    //return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    //}

    void DrawFieldOfView()
    {
        Vector3 frontLeft = DirectionFromAngle(-angle / 2 + transform.eulerAngles.y) * radius;
        Vector3 frontRight = DirectionFromAngle(angle / 2 + transform.eulerAngles.y) * radius;


        int stepCounts = segments + 1;
        float stepAngleSize = angle / (float)segments;
        List<Vector3> vertices = new List<Vector3> { Vector3.up * 0.25f }; // Point central du cône * 2.5f
        List<int> triangles = new List<int>();
        for (int i = 0; i <= stepCounts; i++)
        {
            //float currentAngle = -angle / 2 + stepAngleSize * i;
            //Vector3 vertex = DirectionFromAngle(transform.eulerAngles.y, currentAngle) * radius + (Vector3.forward * 0.7f * 0.25f); // * 0.7f
            //vertices.Add(vertex.normalized * radius * (1 / transform.localScale.x));
            float currentAngle = -angle / 2 + stepAngleSize * i;
            Vector3 vertex = DirectionFromAngle(currentAngle + transform.eulerAngles.y + 185f) * radius + Vector3.up * 0.25f  ;
            vertices.Add(vertex);
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
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        meshRenderer.GetPropertyBlock(mpb);
        if (canSeePlayer)
        {
            mpb.SetColor("_Color", Color.red);
        }
        else
        {
            mpb.SetColor("_Color", Color.yellow);
        }
        meshRenderer.SetPropertyBlock(mpb);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == targetObject1 || other.gameObject == targetObject2)
        {
            Debug.Log("????");
            //buzz.transform.rotation *= Quaternion.Euler(0f, 180f, 0f);
            transform.rotation *= Quaternion.Euler(0f, 180f, 0f);
            Debug.Log(center.transform.position);
            //StartCoroutine(UpdateFOVNextFrame());
        }

    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = Titi.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(Titilancement))
        {
            if (!dessinDebut)
            {
                DrawFieldOfView();
                dessinDebut = true;
            }
            pied.transform.position += pied.transform.forward * Time.deltaTime *0.75f;
        }
        if (alert < 100)
        {
            alertAtMaxAlreadySent = true;
        }

        if (alert >= 100 && !alertAtMaxAlreadySent)
        {
            alertAtMaxAlreadySent = true;
            OnAlertMax?.Invoke();
        }
    }
}
