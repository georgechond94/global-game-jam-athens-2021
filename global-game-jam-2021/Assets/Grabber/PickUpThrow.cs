using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpThrow : MonoBehaviour
{

    public Transform ObjectHolder;
    public float ThrowForce;
    public bool carryObject;
    public GameObject Item;
    public bool IsThrowable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire3"))
        {
            RaycastHit hit;
            Ray directionRay = new Ray(transform.position, transform.forward);
            if(Physics.Raycast(directionRay,out hit, 2f))
            {
                if(hit.collider.tag == "Object")
                {
                    carryObject = true;
                    IsThrowable = true;
                    if(carryObject == true)
                    {
                        Item = hit.collider.gameObject;
                        Item.transform.SetParent(ObjectHolder);
                        Item.gameObject.transform.position = ObjectHolder.position;
                        Item.GetComponent<Rigidbody>().isKinematic = true;
                        Item.GetComponent<Rigidbody>().useGravity = false;
                    }
                }
            }    
        
        }
      
        if(carryObject == false)
        {
            ObjectHolder.DetachChildren();
            Item.GetComponent<Rigidbody>().isKinematic = false;
            Item.GetComponent<Rigidbody>().useGravity = true;
        }
        if(Input.GetButton("Fire1"))
        {
            if (IsThrowable)
            {
                ObjectHolder.DetachChildren();
                Item.GetComponent<Rigidbody>().isKinematic = false;
                Item.GetComponent<Rigidbody>().useGravity = true;
                Item.GetComponent<Rigidbody>().AddForce(transform.forward * ThrowForce);
            }
           
        }
    }
}
