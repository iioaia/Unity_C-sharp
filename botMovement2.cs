using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


// This script controls the Bot Movements. Attach this script  botMovement2.cs  to your bot prefab. 
// Working with the resourceList.cs you can assign a NavMeshAgent to go to specific points in the Scene. 
// YOU NEED the resourceList.cs  script attached to an always present game object. Empty GameObject works.



public class botMovement2 : MonoBehaviour
{

    public float distanceToResource;
    public float timeAtResource;
    public bool isAtResource;
    public float resourceRadius;
    public bool lookingForResource;

    Vector3 distanceBetweenRes;

    private NavMeshAgent navMeshAgent;
    private float timer;

    int currentPosition = 0;

// You need to attach the  resourceList.cs  script to an object that will be in always be in your Scene.
// On that object you must assign objects in the Resource Objects list. The NavMesh Agent uses those for Vector3 positions
    GameObject[] resourceObjects;
   


    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
        timer = timeAtResource;
        resourceList resourceList = GameObject.Find("Manager").GetComponent<resourceList>();
        resourceObjects = resourceList.resourceObjects;
        /*
        //debug log resourceObjects
        Debug.Log(resourceObjects.Length);
        Debug.Log(resourceObjects[0]);
        Debug.Log(resourceObjects[1]);
        Debug.Log(resourceObjects[2]);
        Debug.Log(resourceObjects[0].transform.position);
        Debug.Log(resourceObjects[1].transform.position);
        Debug.Log(resourceObjects[2].transform.position);
        */


    }



    void Update()
    {
        MoveToResource();
    }




    //move to vector3 resourcelist[] position
    void MoveToResource()
    {


        // move to resourceobjects[] position
        navMeshAgent.SetDestination(resourceObjects[currentPosition].transform.position);
        //check if at resourceobjects[] position
        distanceBetweenRes = resourceObjects[currentPosition].transform.position - transform.position;
        //when distanceBetweenRes is less than resourceRadius then isAtResource = true
        if (distanceBetweenRes.magnitude < resourceRadius)
        {
            isAtResource = true;
            Debug.Log("isAtResource");
            //countdown timer timeAtResource
            timer -= Time.deltaTime;
            //if timer is less than 0   then isAtResource = false
            if (timer < 0)
            {
                isAtResource = false;
                timer = timeAtResource;
                if (currentPosition < resourceObjects.Length - 1)
                {
                    currentPosition++;

                }
                else
                {
                    currentPosition = 0;
                
                }
                
                Debug.Log("isAtResource = false");
            }

        }




    }
    
    
}
