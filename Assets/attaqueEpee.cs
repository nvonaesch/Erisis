using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attaqueEpee : MonoBehaviour
{
    public int damage = 100; 

    private void OnTriggerEnter(Collider other)
    {
        VieMonstre monster = other.GetComponent<VieMonstre>();
        if (monster != null)
        {
            monster.TakeDamage(damage);
        }
    }
}
