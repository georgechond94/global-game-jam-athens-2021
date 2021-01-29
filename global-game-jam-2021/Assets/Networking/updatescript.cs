using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updatescript : Bolt.EntityBehaviour<ICustomCubeState>
{
    private int movementSpeed = 10;
    // Start is called before the first frame update
    

    public override void Attached()
    {
        state.SetTransforms(state.CubeTransform, transform);
    }

    // Update is called once per frame
    public override void SimulateOwner()
    {
        //_transform.position.Set(_transform.position.x + 0.01f, _transform.position.y, _transform.position.z);

        //get the Input from Horizontal axis
        float horizontalInput = Input.GetAxis("Horizontal");
        //get the Input from Vertical axis
        float verticalInput = Input.GetAxis("Vertical");

        //update the position
        transform.position = transform.position + new Vector3(horizontalInput * movementSpeed * BoltNetwork.FrameDeltaTime, verticalInput * movementSpeed * BoltNetwork.FrameDeltaTime, 0);

        //output to log the position change
        Debug.Log(transform.position);
    }
}
