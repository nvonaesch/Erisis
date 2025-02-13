using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marsupilami : MonoBehaviour
{
    public GameObject buzz;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        buzz.transform.Rotate(0, 45f, 0);

    }
}
