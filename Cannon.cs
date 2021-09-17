using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannons : MonoBehaviour
{

// When attached to a Game Object this script will cause that object to Aim at the Target (Player) when the Target is within range. 
// It will Launch a projectile at the Target when within the Attack Range.
// Assign the Target, Projectile and Projectile Spawn Position in the Inspector.
// Rigibodies are used for the Projectile.
// Can be used as a Cannon or Turret.

    [SerializeField] Transform target;
    [SerializeField] float targetRange;
    [SerializeField] float attackRange;
    [SerializeField] Rigidbody projectile;
    [SerializeField] float rotationSpeed;
    [SerializeField] Transform projectileSpawnPosition;
    [SerializeField] private float waitTimer;
    [SerializeField] float projectileForce = 2000f;
    
    bool isAllowedToFire = true;

    public Vector3 origin;  // Original Transform Position of Game Object to which this script is attached.
    public Quaternion originalRotation;
    
    float timer;
    float distanceToTarget = Mathf.Infinity;



// Store the original position and rotation
    void Awake()
    {
        origin = gameObject.transform.position;
        originalRotation = gameObject.transform.rotation;    
    }


    void Update()
    {
        timer += Time.deltaTime;
        distanceToTarget = Vector3.Distance(target.position, transform.position); 
        AimToTarget();
        IsAllowedToFire();
        LaunchProjectile();
    }


    private void AimToTarget()
    {
        if (distanceToTarget <= targetRange)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed); 
        }
// This next part needs adjusting for possible performance. When run it generates a debug console until the Transform Rotation reaches 0,0,0.  
        else if (gameObject.transform.rotation != originalRotation) 
        {
            Vector3 direction = (origin - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }


// Verify that the Game Object is allowed to fire the Projectile.  Used to create a delay between Projectiles.
    void LaunchProjectile()
    {
        if (isAllowedToFire == true)
        {
            LaunchProjectileWithinRange();
        }
    }


// Is allowed to Fire a projectile after the timer.
    void IsAllowedToFire()
    {
        if (timer >= waitTimer)
        {
            isAllowedToFire = true;
            timer = 0;
        }
    }


// Spawn the projectile prefab as a Rigidbody and Add  Force.
    private void LaunchProjectileWithinRange()
    {
        if (distanceToTarget <= attackRange)
        {
            Rigidbody projectileInstance;
            projectileInstance = Instantiate(projectile, projectileSpawnPosition.position, projectileSpawnPosition.rotation) as Rigidbody;
            projectileInstance.AddForce(projectileSpawnPosition.forward * projectileForce);
            isAllowedToFire = false;
        }
    }


}
