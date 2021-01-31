using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerAnimatorScript : Bolt.EntityBehaviour<IFarmerState>
{
    // private Animator animator;
    // Start is called before the first frame update


    public override void Attached()
    {
        state.SetAnimator(GetComponent<Animator>());
    }


    // Update is called once per frame
    public override void SimulateOwner()
    {
        if (entity.IsOwner)
        {

            state.isWalking = false;
            state.isWalkingBackwards = false;
            state.isTurningRight = false;
            state.isTurningLeft = false;
            state.isRunning = false;

            if (Input.GetAxis("Horizontal") > 0f)
            {
                state.isTurningRight = true;
                return;
            }

            if (Input.GetAxis("Horizontal") < 0f)
            {
                state.isTurningLeft = true;
                return;
            }

            if (Input.GetAxis("Vertical") > 0f && Input.GetKey(KeyCode.LeftShift))
            {
                state.isRunning = true;
                return;

            }

            if (Input.GetAxis("Vertical") > 0f && !Input.GetKey(KeyCode.LeftShift))
            {
                state.isWalking = true;
                return;

            }

            if (Input.GetAxis("Vertical") < 0f)
            {
                state.isWalkingBackwards = true;

            }

        }
    }
    public void Update()
    {


        state.Animator.SetBool("IsTurningRight", state.isTurningRight);

        state.Animator.SetBool("IsTurningLeft", state.isTurningLeft);

        state.Animator.SetBool("IsRunning", state.isRunning);

        state.Animator.SetBool("IsWalking", state.isWalking);

        state.Animator.SetBool("IsWalkingBackwards", state.isWalkingBackwards);

    }
}
