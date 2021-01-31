
using System.Threading.Tasks;
using Bolt;
using Cinemachine;
using UnityEngine;

public class Shooting : Bolt.EntityBehaviour<IFarmerState>
{
    public Rigidbody bulletPrefab;
    public float bulletSpeed;
    public int ammo = 2;
    public float initBulletSpeed;
    private Animator animator;

    public override void Attached()
    {
        initBulletSpeed = bulletSpeed;
        state.OnShoot = Shoot;
        state.OnPickUp = PickUp;

        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        
        if(Input.GetButtonDown("Fire1") && entity.IsOwner)
        {
            state.Shoot();
        }

        if (Input.GetKeyDown(KeyCode.F) && entity.IsOwner)
        {
            state.PickUp();
        }
    }

    private async void Shoot()
    {

        if (ammo > 0)
        {

            var activeCamera = Object.FindObjectOfType<CinemachineBrain>();

            float x = Screen.width / 2;
            float y = Screen.height / 2;

            var ray = activeCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y, 0));
            animator.SetTrigger("IsThrowing");
            await Task.Delay(800);

            var muzzle = transform.Find("GFX/Muzzle");
            //var bulletClone = BoltNetwork.Instantiate(bulletPrefab.gameObject, muzzle.transform.position, this.transform.rotation);
            Rigidbody bulletClone = Instantiate(bulletPrefab, muzzle.transform.position, this.transform.rotation);
            bulletClone.velocity = ray.direction * bulletSpeed;

            //bulletClone.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);

            // bulletClone.velocity = transform.TransformDirection(new Vector3(0, 0, bulletSpeed));
            ammo--;
        }
    }

    private void PickUp()
    {
        var item = FindClosest("Throwable", 20f, out float dist);
        //Ray directionRay = new Ray(grabber.transform.position, -grabber.transform.up);
        if (item != null)
        {

            ammo++;
            //Destroy(hit.collider.gameObject);
            BoltNetwork.Destroy(item);
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
