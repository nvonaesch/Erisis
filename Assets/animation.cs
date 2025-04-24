using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation : MonoBehaviour
{
    private Animator animator;
    private bool alreadyTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            animator.SetBool("goToBagarre", true);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetBool("goToBagarre", false);
            animator.SetBool("goToCasting", true);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("goToBagarre", false);
            animator.SetBool("goToCasting", false);
        }

    }
}
