using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookDoor : MonoBehaviour
{
    public GameObject door;
    public Boolean isLocked = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(isLocked == false)
        {
            door.transform.Rotate(0f, 90f, 0f);
            isLocked = true;
        }
    }
}
