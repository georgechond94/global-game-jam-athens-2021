
using UnityEngine;

public class Shooting : Bolt.EntityBehaviour<IFarmerState>
{
    public Rigidbody bulletPrefab;
    public float bulletSpeed;
    public int ammo = 2;

    public override void Attached()
    {
        state.OnShoot = Shoot;
        state.OnPickUp = PickUp;
    }
    private void Shoot()
    {

        if (ammo > 0)
        {
            var muzzle = transform.Find("GFX/Muzzle");
            Rigidbody bulletClone = Instantiate(bulletPrefab, muzzle.transform.position, this.transform.rotation);
            bulletClone.AddForce(transform.forward * bulletSpeed);

           // bulletClone.velocity = transform.TransformDirection(new Vector3(0, 0, bulletSpeed));
            ammo--;
        }
        
       
        
    }

    public void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Z) && entity.IsOwner)
        {
            state.Shoot();
        }

        if (Input.GetButton("Fire3") && entity.IsOwner)
        {
            state.PickUp();
        }
    }
    private void PickUp()
    {
            var grabber = transform.Find("Grabber");  
            Ray directionRay = new Ray(grabber.transform.position, grabber.transform.forward);
            if (Physics.Raycast(directionRay, out var hit, 2f))
            {

                if (hit.collider.tag == "Throwable")
                {
                ammo++;
                Destroy(hit.collider.gameObject);
               
                }
           
            }
       
        

    }
}
