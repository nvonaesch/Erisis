using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VieMonstre : MonoBehaviour
{
    public int maxHealth = 100; 
    private int currentHealth; 
    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth; 
        animator = GetComponent<Animator>(); 
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Monstre touché");

        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die(){
        animator.Play("Mort"); 
        yield return new WaitForSeconds((float)0.5);
        gameObject.SetActive(false);
        GetComponent<Collider>().enabled = false;
    }
}
