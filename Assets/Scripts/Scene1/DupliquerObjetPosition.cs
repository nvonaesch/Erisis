using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class DupliquerObjetPosition : MonoBehaviour
{
   
    public GameObject objADupliquer;
    void Start()
    {
        StartCoroutine(Dupliquer());
    }

    IEnumerator Dupliquer()
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForSeconds((float)0.1);

            Vector3[] positions = {
            new Vector3((float)-0.578516,(float)5.486312,(float)-1.106001),
            new Vector3((float)2.85,(float)5.486312,(float)-1.106001),
            new Vector3((float)8.77,(float)5.486312,(float)-1.106001),
            new Vector3((float)-3.58,(float)5.486312,(float)-1.106001),
            new Vector3((float)-8.51,(float)5.486312,(float)-1.106001),
        };

            Vector3 position = positions[Random.Range(0, positions.Length)];
            GameObject duplicata = Instantiate(objADupliquer, position, objADupliquer.transform.rotation);

            Destroy(duplicata.GetComponent<DupliquerObjetPosition>());
        }
    }
}
