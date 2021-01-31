using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerAnimatorScript : Bolt.EntityBehaviour<IFarmerState>
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (entity.IsOwner)
        {


            foreach (AnimatorControllerParameter parameter in animator.parameters)
            {
                if (parameter.type == AnimatorControllerParameterType.Bool)
                {
                    animator.SetBool(parameter.name, false);
                }
            }

            if (Input.GetAxis("Horizontal") > 0f)
            {
                animator.SetBool("IsTurningRight", true);
                return;
            }

            if (Input.GetAxis("Horizontal") < 0f)
            {
                animator.SetBool("IsTurningLeft", true);
                return;
            }

            if (Input.GetAxis("Vertical") > 0f && Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("IsRunning", true);
                return;

            }

            if (Input.GetAxis("Vertical") > 0f && !Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("IsWalking", true);
                return;

            }

            if (Input.GetAxis("Vertical") < 0f)
            {
                animator.SetBool("IsWalkingBackwards", true);

            }
        }
    }
}
