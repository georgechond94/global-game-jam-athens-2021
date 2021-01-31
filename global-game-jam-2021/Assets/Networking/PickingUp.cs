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
        var item = FindClosest("Grabbable", 20f, out float dist);

        if (Item == null && item)
        {
            var grb = transform.Find("Grabber");
                Item = item;
                Item.transform.SetParent(grb);
                Item.transform.position = grb.transform.position;
                Item.GetComponent<Rigidbody>().isKinematic = true;
                Item.GetComponent<Rigidbody>().useGravity = false;
        }
    }


    public GameObject FindClosest(string tag, float threshold, out float dist)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(tag);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance && curDistance < threshold)
            {
                closest = go;
                distance = curDistance;
            }
        }
        dist = distance;
        return closest;
    }
}
