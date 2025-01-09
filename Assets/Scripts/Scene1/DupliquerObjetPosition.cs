using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class DupliquerObjetPosition : MonoBehaviour
{
    public GameObject objADupliquer;
    private GameObject duplicata;
    private Vector3 localPosition;
    void Start()
    {
        StartCoroutine(Dupliquer());
        duplicata = objADupliquer;
    }

    IEnumerator Dupliquer()
    {
        yield return new WaitForSeconds(5);
        localPosition = objADupliquer.transform.localPosition;
        localPosition.y+=1; 
        duplicata.transform.SetLocalPositionAndRotation(localPosition, objADupliquer.transform.localRotation);
        Destroy(duplicata.GetComponent("DupliquerObjetPosition"));
        Instantiate (duplicata, duplicata.transform);   
    }
}
