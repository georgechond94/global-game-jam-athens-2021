using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpThrow : MonoBehaviour
{
    public float ThrowForce;
    private bool carryObject;
    private GameObject Item;
    private bool IsThrowable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Item == null && Input.GetButton("Fire3"))
        {
            Ray directionRay = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(directionRay, out var hit, 2f))
            {
                if (hit.collider.tag == "Object")
                {
                    carryObject = true;
                    IsThrowable = true;

                    Item = hit.collider.gameObject;
                    Item.transform.SetParent(transform);
                    Item.gameObject.transform.position = transform.position;
                    Item.GetComponent<Rigidbody>().isKinematic = true;
                    Item.GetComponent<Rigidbody>().useGravity = false;

                }
            }

        }

        if (Item != null && Input.GetButton("Fire1") && IsThrowable)
        {
            transform.DetachChildren();
            Item.GetComponent<Rigidbody>().isKinematic = false;
            Item.GetComponent<Rigidbody>().useGravity = true;
            Item.GetComponent<Rigidbody>().AddForce(transform.forward * ThrowForce);
            Item = null;
            carryObject = true;
            IsThrowable = true;
        }
    }
}
