using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // Move left and right 
    Transform cannonTransform;
    [SerializeField] float turretBarrelRotationSpeed = 4f;
    [SerializeField] float turretFiringRadius = 4f;
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileLifetime = 0.5f;
    [SerializeField] float reloadTimerMax = 1f;
    [SerializeField] float fov = 45f;
    private Vector3 turretDirection;
    bool hasTarget = false;
    float reloadTimer;
    private bool isReloading = true;

    private void Awake()
    {
        cannonTransform = transform.Find("cannon");

    }
    private void Start()
    {
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(cannonTransform.position, cannonTransform.up);
        Gizmos.DrawWireSphere(transform.position, turretFiringRadius);
    }
    private void ScanForTarget()
    {
        if (!hasTarget)
        {
            turretDirection = new Vector3(0, 0, Mathf.PingPong(Time.time * turretBarrelRotationSpeed, fov) - (fov / 2));
            cannonTransform.localEulerAngles = turretDirection;
        }
        if (cannonTransform != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(cannonTransform.position, cannonTransform.up);

            if (hit.collider != null)
            {
                hasTarget = true;
                //check distance to enemy 
                bool isInRange = hit.distance < turretFiringRadius;
                if (hit.collider.gameObject.tag == "Enemy" && isInRange)
                {
                    if (!isReloading)
                    {
                        Fire();
                    }

                }
            }
            else
            {
                hasTarget = false;
            }
        }
    }
    private void Fire()
    {
        hasTarget = true;
        Debug.Log("Firing");
        //Find the muzzle of the turret
        Transform muzzle = transform.Find("cannon").Find("muzzle");
        // Instantiate bullet pf from projectile transform point
        Debug.Log(muzzle);
        Projectile bullet = Instantiate(projectile, muzzle).GetComponent<Projectile>();

        // Add force in direction of the cannonTransform up vector
        bullet.GetComponent<Rigidbody2D>().AddForce(cannonTransform.up * projectileSpeed);
        // Destroy after specified lifetime
        bullet.Die(projectileLifetime);
        //reload 
        reloadTimer += reloadTimerMax;
        isReloading = true;
        
    }

    private void Update()
    {
        //Decrement reload timer
        if (reloadTimer >= 0)
        {
            reloadTimer -= Time.deltaTime;

        }
        else
        {
            // Shoot
            isReloading = false;
        }
    }

    private void FixedUpdate()
    {// Spin the object around the target at 20 degrees/second.
        ScanForTarget();

    }
}
