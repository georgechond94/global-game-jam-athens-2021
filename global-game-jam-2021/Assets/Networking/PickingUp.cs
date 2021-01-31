using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingUp : Bolt.EntityBehaviour<IBirdState>
{
    private GameObject Item;

    public override void Attached()
    {
        state.OnShoot = Shoot;
        state.OnPickUp = PickUp;
    }

    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.F) && entity.IsOwner && Item != null)
        {
            state.Shoot();
        }

        if (Input.GetKeyDown(KeyCode.F) && entity.IsOwner && Item == null)
        {
            state.PickUp();
        }
    }

    private void Shoot()
    {
        if (Item != null)
        {
            Item.transform.parent = null;
            Item.GetComponent<Rigidbody>().isKinematic = false;
            Item.GetComponent<Rigidbody>().useGravity = true;
            Item.GetComponent<Rigidbody>().AddForce(transform.forward);
            Item = null;
        }
    }

    private void PickUp()
    {
        var grabber = transform.Find("Grabber");
        Ray directionRay = new Ray(grabber.transform.position, -grabber.transform.up);
        if (Item == null && Physics.Raycast(directionRay, out var hit, 200f))
        {
            if (hit.collider.tag == "Grabbable")
            {
                Item = hit.collider.gameObject;
                Item.transform.SetParent(transform);
                Item.transform.position = transform.position;
                Item.GetComponent<Rigidbody>().isKinematic = true;
                Item.GetComponent<Rigidbody>().useGravity = false;
            }

        }
    }
}
