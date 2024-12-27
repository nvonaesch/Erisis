using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersJoueur : MonoBehaviour
{
    private GameObject destination;
    private UnityEngine.AI.NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        destination = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(destination.transform.position);
    }
}
