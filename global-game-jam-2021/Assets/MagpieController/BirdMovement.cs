using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BirdMovement : Bolt.EntityBehaviour<IBirdState>
{


    Rigidbody mybody;
    private float speed;
    public float walkSpeed;
    public float runSpeed;
    public float rotateSpeed;
    bool canJump;

    public float jumpForce;

    private Cinemachine.CinemachineBrain camera;


    // Start is called before the first frame update
    void Start()
    {
        mybody = GetComponent<Rigidbody>();
        camera = Resources.FindObjectsOfTypeAll<CinemachineBrain>().FirstOrDefault();
        speed = walkSpeed;
    }

    void Update()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Vector3 moveDirection = new Vector3(0f, 0f, vertical) * speed * Time.deltaTime;
        var jumping = canJump ? 1f : 0f;
        Vector3 moveDirection = new Vector3(horizontal, jumping, vertical) * speed * BoltNetwork.FrameDeltaTime;
        //Vector3 rotateDirection = new Vector3(0f, horizontal * rotateSpeed, 0f);
        transform.Translate(moveDirection);
       
        if (vertical != 0f)
        {
                transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(
                camera.transform.rotation.x,
                camera.transform.rotation.y,
                camera.transform.rotation.z,
                camera.transform.rotation.w), rotateSpeed * BoltNetwork.FrameDeltaTime);
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
        if (Input.GetKey(KeyCode.Space))
        {
           // canJump = !canJump;
           // Vector3 jumpDirection = new Vector3(0f, 1f, 0f) * speed * BoltNetwork.FrameDeltaTime;
            //Vector3 rotateDirection = new Vector3(0f, horizontal * rotateSpeed, 0f);
            //transform.Translate(jumpDirection);
        }
      
    }
  

}
