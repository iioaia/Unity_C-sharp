using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveToPositionEngine : MonoBehaviour
{



// When a Player enables the trigger event this script will Move an object to a position and stop when it reaches it.

// Attach this script to your Moving platform / Object to be moved.

// In the Inspector for the Moving Object:
//  Find the 'Positions' field, click + once.   
//   Assign the  'Position'  object.
//     Assign the Player object. Ensure Player has the Player Tag.
// Place an empty game object in the Scene and attach the ObjectMovementTrigger script to it. Suggested to use 1 Position per Moving Object.




    public GameObject[] positions;    // Position(s) the platform will move to.
    public GameObject player;       
    int currentPosition = 0;           // If using more than 1 position.
    public float PlatformMovementSpeed;
    [SerializeField] float WaypointRadius = 1;  // Buffer around warpoint to avoid warping and surpassing the point.
    public bool playerTriggered = false;        // Has the Trigger been activated by the Player.  False on Trigger Exit. This prevents non player objects from triggering movement.
  
    public GameObject ObjectMovementTrigger;

    

    void Update()
    {
        PlatformMoveToPoint();
    }






    void PlatformMoveToPoint()
    {
    
    // if the Distance between the current waypoint and the target waypoint is less than 1 or Radius, the currentPosition will increase by 1. Only used if using multiple positions.

        if (Vector3.Distance(positions[currentPosition].transform.position, transform.position) < WaypointRadius)
        {
            currentPosition++;

            if (currentPosition >= positions.Length)
            {
                currentPosition = 0;
            }
        }

        if (playerTriggered != false)       
        {

            // Move the object or platform to the next Waypoint in the list. If 0, return to start or end anchor.
            gameObject.transform.position = Vector3.MoveTowards(transform.position, positions[currentPosition].transform.position, Time.deltaTime * PlatformMovementSpeed); 
        }  
        
        


    }


}

