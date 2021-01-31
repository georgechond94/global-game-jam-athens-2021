using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

public class Farmer_Controller : Bolt.EntityBehaviour<IFarmerState>
{
    Farmer_Input farmerInput;
    Rigidbody mybody;

    private float speed;
    public float walkSpeed;
    public float runSpeed;
    public float rotateSpeed;


    public float jumpForce;

    public Transform groundCheck;
    CapsuleCollider theCollider;
    private Animator animator;

    private Cinemachine.CinemachineBrain camera;

    bool isCrouched;
    bool isGrounded;
    bool canJump;

    private bool isMoving = false;
    // Start is called before the first frame update
    public override void Attached()
    {
        animator = GetComponent<Animator>();

        camera = Resources.FindObjectsOfTypeAll<CinemachineBrain>().FirstOrDefault();

        farmerInput = GetComponent<Farmer_Input>();
        mybody = GetComponent<Rigidbody>();

        theCollider = GetComponent<CapsuleCollider>();

        Cursor.lockState = CursorLockMode.Locked;
        speed = walkSpeed;
    }

    // Update is called once per frame
    public override void SimulateOwner()
    {
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Vector3 moveDirection = new Vector3(0f, 0f, vertical) * speed * Time.deltaTime;
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical) * speed * BoltNetwork.FrameDeltaTime;
        //Vector3 rotateDirection = new Vector3(0f, horizontal * rotateSpeed, 0f);
        transform.Translate(moveDirection);
        if (vertical != 0f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(
                transform.rotation.x, 
                camera.transform.rotation.y,
                transform.rotation.z,
                camera.transform.rotation.w), rotateSpeed * BoltNetwork.FrameDeltaTime);
        }

        isGrounded = Physics.CheckSphere(groundCheck.transform.position, 0.1f);

        if(Input.GetKeyDown(farmerInput.jumpKey) && isGrounded)
        {
            canJump = !canJump;
        }

        if (Input.GetKeyUp(farmerInput.crouchKey))
        {
            DoCrouch();
        }

        if (Input.GetKeyDown(farmerInput.run) && !isCrouched)
        {
            speed = runSpeed;
        }

        if (Input.GetKeyUp(farmerInput.run))
        {
            speed = walkSpeed;
        }
    }

    void FixedUpdate()
    {
        if (canJump)
        {
            mybody.AddForce(Vector3.up * jumpForce);
            canJump = !canJump;
        }
    }
    void DoCrouch()
    {
        if(isCrouched)
        {
            theCollider.height += 1f;
        } else
        {
            theCollider.height -= 1f;
        }
        isCrouched = !isCrouched;
    }
}
