using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class animation : MonoBehaviour
{
    public Animator animator;
    private string moveStateName1 = "Walk 0";
    private string moveStateName = "walk";
    public GameObject Hypnos_object;
    public Vector3 positionInvocation;
    public Animator Hypnos;
    public Animator Titi;
    private string Titilancement = "Walk";
    private string Hypnoslancement = "Magic";
    private bool alreadyTriggered = false;
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
    public bool hasAlerted = false;
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

    // Start is called before the first frame update
    void Start()
    {
        Hypnos_object.SetActive(false);
        animator = GetComponentInChildren<Animator>();
        StartCoroutine(FOVRoutine());

        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Standard"));
        mesh = new Mesh();
        meshFilter.mesh = mesh;
        //DrawFieldOfView();

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
            alert += 10;
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
        List<Vector3> vertices = new List<Vector3> { Vector3.up  *0.25f}; // Point central du cône * 2.5f
        List<int> triangles = new List<int>();
        for (int i = 0; i <= stepCounts; i++)
        {
            float currentAngle = -angle / 2 + stepAngleSize * i;
            Vector3 vertex = DirectionFromAngle(transform.eulerAngles.y, currentAngle) * radius + (Vector3.up *0.7f * 0.25f); // * 0.7f
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
            Debug.Log("n3ardinmouk");
            buzz.transform.rotation *= Quaternion.Euler(0f, 180f, 0f);
            Debug.Log(center.transform.position);
            //StartCoroutine(UpdateFOVNextFrame());
        }

    }
    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(moveStateName) || stateInfo.IsName(moveStateName1))
        {
            if (!dessinDebut)
            {
                DrawFieldOfView();
                dessinDebut = true;
            }
            pied.transform.position += pied.transform.forward * Time.deltaTime;
        }
        if(alert >= 100 && !hasStartedBagarre )
        {
            animator.Play("bagarre");
            hasStartedBagarre = true;
            
        }
        if(hasStartedBagarre && alert < 100)
        {
            animator.Play("walk");
            pied.transform.position += pied.transform.forward * Time.deltaTime;
            hasStartedBagarre=false;
        }
        if (stateInfo.IsName("casting") && !hasAlerted)
        {
            StartCoroutine(Explode());

            
        }
        if (stateInfo.IsName("casting") && hasAlerted && alert>=100)
        {
            StartCoroutine(BOOM());
            
        }

    }

    IEnumerator Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(pied.transform.position, radius, targetMask);
        foreach (Collider hit in colliders) { 
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(power, pied.transform.position, radius);
            }
            Hypnos_object.transform.position = positionInvocation;
            Hypnos_object.SetActive(true);
            positionInvocation = rb.transform.position + rb.transform.forward * 5f;
            Hypnos.Play(Hypnoslancement);
            yield return new WaitForSeconds(3f);
            Hypnos_object.SetActive(false);
            
            hasAlerted = true;
            
        }
    }
    IEnumerator BOOM()
    {
        Collider[] colliders = Physics.OverlapSphere(pied.transform.position, radius, targetMask);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(100000, pied.transform.position, radius, 2f);
            }

        }
        yield return new WaitForSeconds(2f);
        DontDestroyOnLoad(playerRef);
        SceneManager.LoadScene("Scene1", LoadSceneMode.Single);
    }
}
