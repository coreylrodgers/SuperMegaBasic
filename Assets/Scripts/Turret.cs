using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    BuildingTypeSO buildingType;
    Transform cannonTransform;
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] float projectileLifetime = 0.5f;
    private Vector3 turretDirection;
    bool hasTarget = false;
    float reloadTimer;
    private bool isReloading = true;

    private void Awake()
    {
        cannonTransform = transform.Find("cannon");
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;

    }
    void OnDrawGizmos()
    {
        Gizmos.DrawRay(cannonTransform.position, cannonTransform.up);
        Gizmos.DrawWireSphere(transform.position, buildingType.range);
    }
    private void ScanForTarget()
    {
        if (!hasTarget)
        {
            turretDirection = new Vector3(0, 0, Mathf.PingPong(Time.time * buildingType.scanSpeed, buildingType.fov) - (buildingType.fov / 2));
            cannonTransform.localEulerAngles = turretDirection;
        }
        if (cannonTransform != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(cannonTransform.position, cannonTransform.up);

            if (hit.collider != null)
            {
                
                //check distance to enemy 
                bool isInRange = hit.distance < buildingType.range;

                // No target if not in range
                if(!isInRange) {
                    hasTarget = false;
                }
                if (hit.collider.gameObject.tag == "Enemy" && isInRange)
                {
                    hasTarget = true;
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
        //Find the muzzle of the turret
        Transform muzzle = transform.Find("cannon").Find("muzzle");
        // Instantiate bullet pf from projectile transform point
        Projectile bullet = Instantiate(buildingType.projectile, muzzle.position, transform.rotation).GetComponent<Projectile>();

        if(bullet == null) {
            Debug.Log("No projectile found");
        }
    
        // Add force in direction of the cannonTransform up vector
        bullet.GetComponent<Rigidbody2D>().AddForce(cannonTransform.up * projectileSpeed);
        // Destroy after specified lifetime
        bullet.Die(projectileLifetime);
        //reload 
        reloadTimer += buildingType.reloadSpeed;
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
