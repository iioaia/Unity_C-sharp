using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannons : MonoBehaviour
{


// UNFINISHED SCRIPT.  This script will follow the target until the target is out of range. Then the cannon will return to it's original rotation. 
// If the Target comes too close, a projectile will spawn.
// TODO  Add rigidbody force to projectile towwards the target.


    [SerializeField] Transform target;
    [SerializeField] float targetRange;
    [SerializeField] float attackRange;
    [SerializeField] Rigidbody cannonBall;
    [SerializeField] float rotationSpeed;
    [SerializeField] Transform ballSpawnPosition;

    public Vector3 origin;
    public Quaternion originalRotation;
    Vector3 cannonBallSpawnPosition;
    
    bool atOrigin = true;

    float distanceToTarget = Mathf.Infinity;


    void Awake()
    {
        origin = gameObject.transform.position;
        originalRotation = gameObject.transform.rotation;
        

    
    }


    void Update()
    {

       distanceToTarget = Vector3.Distance(target.position, transform.position); 
       AimCannonToTarget();
       LaunchCannonBall();
    }


    private void AimCannonToTarget()
    {
        if (distanceToTarget <= targetRange)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed); 
        }

// This needs adjusting for future performance. When this part is run it generates a debug console until the Transform Rotation reaches 0,0,0.  
        else if (gameObject.transform.rotation != originalRotation) 
        {
            Debug.Log("ref");
            Vector3 direction = (origin - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }

    }


    void LaunchCannonBall()
    {
       

            if (distanceToTarget <= attackRange)
            {
                Rigidbody cannonBallInstance;
                cannonBallInstance = Instantiate(cannonBall, ballSpawnPosition.position, ballSpawnPosition.rotation) as Rigidbody;
                cannonBallInstance.AddForce(ballSpawnPosition.up * 350f);
            }
        
    }


}