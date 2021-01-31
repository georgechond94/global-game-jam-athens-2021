using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectScript : Bolt.EntityBehaviour<IFarmerState>
{
    private RectTransform reticle;

    public float restingSize;

    public float maxSize;

    public float speed;

    private float currectSize;
    // Start is called before the first frame update
    public override void Attached()
    {
        reticle = GetComponent<RectTransform>();
    }

    // Update is called once per frame  
    public override void SimulateOwner()
    {
        if (IsMoving)
        {
            currectSize = Mathf.Lerp(currectSize, maxSize, BoltNetwork.FrameDeltaTime * speed);
        }
        else
        {
            currectSize = Mathf.Lerp(currectSize, restingSize, BoltNetwork.FrameDeltaTime * speed);
        }
        reticle.sizeDelta = new Vector2(currectSize, currectSize);
    }

    bool IsMoving =>
        Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 ||
        Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0;
}
