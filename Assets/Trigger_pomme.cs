using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_pomme : MonoBehaviour{
    public GameObject appleToDisable;
    public GameObject swordToEnable;

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag=="hand"){
            appleToDisable.SetActive(false);
            swordToEnable.SetActive(true);
        }
    }
}
