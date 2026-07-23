using UnityEngine;

public class ArsenalShoot : MonoBehaviour
{
    public float shootTimer = 0f;
    public bool shootFlag = false;
    public Laser lazer;
    /*
    public GameObject bullet;
    public float shootForce, upwardForce;
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool shooting, readyToShoot, reloading;
    public Transform attackPoint;
    */

    public GameObject BulletBase;
    public Transform firePoint;

    public float launchForce = 20f;
    public float fireRate = 0.2f;

    private float nextFireTime = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //BulletBase = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) /*&& Time.time >= nextFireTime*/)
        {
            //nextFireTime = Time.time + fireRate;
            Shoot();
        }
        /*
        if (shootFlag)
        {
            
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit bulletHit;
            Vector3 targetPoint;

            if (Physics.Raycast(ray, out bulletHit))
            {
                targetPoint = bulletHit.point;
            }
            else
            {
                targetPoint = ray.GetPoint(50);
            }
            Vector3 shot direction = targetPoint - attackPoint.position;
            


        }
        else
        {
            
        }*/

    }

    void Shoot()
    {
        GameObject bullet = Instantiate(BulletBase, firePoint.position, firePoint.rotation);
        Rigidbody body = bullet.GetComponent<Rigidbody>();

        if (body != null)
        {
            body.linearVelocity = firePoint.up * launchForce;
        }
    }
}
