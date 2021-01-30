using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird_Controller : MonoBehaviour
{
    Bird_Input birdInput;
    Rigidbody mybody;

    private float speed;
    public float walkSpeed;
    public float runSpeed;
    public float crouchSpeed;



    public float jumpForce;

    public Transform groundCheck;
    CapsuleCollider theCollider;


    bool isCrouched;
    bool isGrounded;
    bool canJump;
    bool canFly;

    // Start is called before the first frame update
    void Start()
    {
        birdInput = GetComponent<Bird_Input>();
        mybody = GetComponent<Rigidbody>();

        theCollider = GetComponent<CapsuleCollider>();

        Cursor.lockState = CursorLockMode.Locked;
        speed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical) * speed * Time.deltaTime;
        transform.Translate(moveDirection);

        isGrounded = Physics.CheckSphere(groundCheck.transform.position, 0.1f);

        if(Input.GetKeyDown(birdInput.jumpKey) && isGrounded)
        {
            canJump = !canJump;
        }

        if (Input.GetKeyUp(birdInput.crouchKey))
        {
            DoCrouch();
        }

        if (Input.GetKeyDown(birdInput.run) && !isCrouched)
        {
            speed = runSpeed;
        }

        if (Input.GetKeyUp(birdInput.run))
        {
            speed = walkSpeed;
        }

        if(Input.GetKeyDown(birdInput.toggleFly))
        {
            fly();
        }

        if (Input.GetKey(birdInput.flyUp) && canFly)
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }

        if (Input.GetKey(birdInput.flyDown) && canFly)
        {
            transform.position -= transform.up * speed * Time.deltaTime;
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

    void fly()
    {
        canFly = !canFly;
        mybody.isKinematic = canFly;
    }
}
