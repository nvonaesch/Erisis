using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marsupilami2 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject buzz;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        buzz.transform.Rotate(0, 45f, 0);
    }
}
