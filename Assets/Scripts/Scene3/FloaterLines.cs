using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject targetObject; 
    public float moveDistance = 5f; 

    void Update()
    {
        if (targetObject != null)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                targetObject.transform.position += Vector3.left * moveDistance;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                targetObject.transform.position += Vector3.right * moveDistance;
            }
        }
    }
}
