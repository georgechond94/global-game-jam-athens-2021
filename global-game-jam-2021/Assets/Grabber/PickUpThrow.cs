using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpThrow : MonoBehaviour
{
    public float ThrowForce;
    private bool carryObject;
    private GameObject Item;
    private bool IsThrowable;
    public string targetTag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Item == null && Input.GetKeyDown(KeyCode.F))
        {
            Ray directionRay = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(directionRay, out var hit, 400f))
            {
                if (hit.collider.tag == targetTag)
                {
                    Debug.LogError("mpike");
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

        if (Item != null && Input.GetButtonDown("Fire1") && IsThrowable)
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
