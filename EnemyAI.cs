using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    // This script will cause a NavMeshAgent object to move to random points within a certain distance from it's current position.
    // See details below for non random, fixed positions.
    // If the object Detects the player or Target it will begin to chase after the target until it reaches the target's current position.
    // If no target is detected, the object will resume it's random movement.



    NavMeshAgent navMeshAgent;

    float distanceToTarget = Mathf.Infinity;
    

    [SerializeField] Transform target;  // In the Inspector you must assign a Target for the object to follow / chase. e.g: The Player
    [SerializeField] float chaseRange = 5f;    //   The distance in which the object is looking for the Target.
    
    [SerializeField] bool isProvoked;    //   Has the object detected the target? True or False.

    [HideInInspector] public float timer;     
    public float wanderTimer;         // Length of time the object will remain at a New Patrol Position before moving to the next.
    public float wanderRadius;        // How far from current position can the next position be.
    public Vector3 newPatrolPosition; //  The next position the object will move to.


    // Below is for Waypoints instead of Random Movement points. 
    // To use Waypoints  you MUST tick  'Use Waypoints' in the Inspector.
    // The object will use the navmesh to navigate around obstacles until it reaches a set destination waypoint.
    // If the object detects the target or Player it will chase the target. If target leaves the range, the object will resume waypoint movement.

    public GameObject[] waypointPositions;   // Creates a list in the Inspector where you MUST assign target Waypoints.
    int currentPosition = 0; 
    public bool useWaypoints;
    float WaypointRadius; 



    void Start()
    {
        timer = wanderTimer;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }


// Debug purposes. Shows a red wireframe sphere in the Unity editor representing the chase range.
    void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
    

    void Update()
    {
        timer += Time.deltaTime;
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (useWaypoints == true)
        {  
            WaypointRadius = 1;            
            MoveToWaypoints();
        
            if (distanceToTarget <= chaseRange)
            {
                isProvoked = true;
                EngageTarget();
            }
            
        }


        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
            EngageTarget();
        }

        else if (distanceToTarget >= chaseRange)
        {
            RandomPatrol();
            isProvoked = false;
        }

    }


    void RandomPatrol()
    {
        if (timer >= wanderTimer)
        {
            newPatrolPosition = RandomNavSphere(transform.position, wanderRadius, -1);  // This sets the new Patrol Position as a Vector3
            navMeshAgent.SetDestination(newPatrolPosition); 
            timer = 0;
        }
    }



    // This method will obtain the next Random Position and make that new position the object's origin.
    // The origin is then used to calculate the next position.

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)    // float dist = wanderRadius.
     {
        Vector3 randomDirection = Random.insideUnitSphere * dist;
 
        randomDirection += origin;
 
        NavMeshHit randomPointReached;
 
        NavMesh.SamplePosition (randomDirection, out randomPointReached, dist, layermask);
 
        return randomPointReached.position;
    }

    

    private void EngageTarget()
    {
        navMeshAgent.SetDestination(target.position);
    }




    private void MoveToWaypoints()
    {
        if (Vector3.Distance(waypointPositions[currentPosition].transform.position, transform.position) < WaypointRadius)
        {
            currentPosition++;
        }
        if (currentPosition >= waypointPositions.Length)
        {
            currentPosition = 0;
        }

        navMeshAgent.SetDestination(waypointPositions[currentPosition].transform.position);
    }



}
