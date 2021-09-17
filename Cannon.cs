using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannons : MonoBehaviour
{

// 
// 
// 

    [SerializeField] Transform target;
    [SerializeField] float targetRange;
    [SerializeField] float attackRange;
    [SerializeField] Rigidbody projectile;
    [SerializeField] float rotationSpeed;
    [SerializeField] Transform projectileSpawnPosition;
    [SerializeField] private float waitTimer;
    [SerializeField] float projectileForce = 2000f;
    float timer;


    bool isAllowedToFire = true;

    public Vector3 origin;
    public Quaternion originalRotation;
    
    

    float distanceToTarget = Mathf.Infinity;


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
// This needs adjusting for possible performance. When this part is run it generates a debug console until the Transform Rotation reaches 0,0,0.  
        else if (gameObject.transform.rotation != originalRotation) 
        {
            Vector3 direction = (origin - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }


    void LaunchProjectile()
    {
        if (isAllowedToFire == true)
        {
            LaunchProjectileWithinRange();
        }

    }


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

    void IsAllowedToFire()
    {
        if (timer >= waitTimer)
        {
            isAllowedToFire = true;
            timer = 0;
        }
    }






}
