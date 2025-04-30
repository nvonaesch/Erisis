using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation : MonoBehaviour
{
    private Animator animator;
    private string moveStateName = "Walk 0";
    private bool alreadyTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(moveStateName))
        {
            transform.position += transform.forward * Time.deltaTime;
        }
    }
}
