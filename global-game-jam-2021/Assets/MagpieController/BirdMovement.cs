using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BirdMovement : Bolt.EntityBehaviour<IBirdState>
{

    public Animator animator;

    private float speed;
    public float walkSpeed;
    public float runSpeed;
    public float rotateSpeed;

    public float jumpForce;

    private bool isFlying = false;
    private Cinemachine.CinemachineBrain camera;
    bool canJump;
    bool canDive;


    // Start is called before the first frame update
    public void Start()
    {
        if (!entity.IsOwner)
        {
            return;
        }
        camera = Resources.FindObjectsOfTypeAll<CinemachineBrain>().FirstOrDefault();
        speed = walkSpeed;
    }

    public void Update()
    {
        if (!entity.IsOwner)
        {
            return;
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Vector3 moveDirection = new Vector3(0f, 0f, vertical) * speed * Time.deltaTime;
        var jumping = canJump ? jumpForce : (canDive ? -jumpForce : 0f);
        Vector3 moveDirection = new Vector3(horizontal, jumping, vertical) * speed * BoltNetwork.FrameDeltaTime;
        //Vector3 rotateDirection = new Vector3(0f, horizontal * rotateSpeed, 0f);
        transform.Translate(moveDirection);
       
        if (vertical != 0f)
        {
            if (!isFlying)
            {
                animator.SetBool("IsMoving", true);
                isFlying = true;
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(
                camera.transform.rotation.x,
                camera.transform.rotation.y,
                camera.transform.rotation.z,
                camera.transform.rotation.w), rotateSpeed * BoltNetwork.FrameDeltaTime);
        }
        else
        {
            if (isFlying)
            {
                animator.SetBool("IsMoving", false);
                isFlying = false;
            }

        }

        if (Input.GetKeyDown(KeyCode.LeftShift) )
        {
            speed = runSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = walkSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            canJump = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            canJump = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            canDive = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            canDive = false;
        }

    }
  

}
