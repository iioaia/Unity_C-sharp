using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovementTrigger : MonoBehaviour
{
    // This script will enable the movement of the object containing the ObjectMoveToPositionEngine script.

    // You must add this current script to every object that will use  ObjectMovementTrigger.
    //
    // Attach this script to an Empty GameObject.
    // In the Inspector for this empty gameobject, assign  the  'Object To Be Moved'  field  with the object   you want to move.

    public ObjectMoveToPositionEngine ObjectToBeMoved;



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))    
        {
            ObjectToBeMoved.playerTriggered = true;
        }
    }






}
