using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor2 : MonoBehaviour
{
    [SerializeField]
    public GameObject door;
    public Boolean isLocked = false;
    public Boolean isOpening = false;
    private float currentAngle = 0f;
    private float targetAngle = 90f;
    private float rotationSpeed = 50f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isOpening && currentAngle < targetAngle)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;
            currentAngle += rotationStep;
            door.transform.Rotate(0f, -rotationStep, 0f);
            if (currentAngle >= targetAngle)
            {
                isOpening = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isLocked == false)
        {
            isLocked = true;
            isOpening = true;
        }
    }
}
