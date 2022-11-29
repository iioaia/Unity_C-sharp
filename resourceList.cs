using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resourceList : MonoBehaviour
{

    public static Vector3 resourceA;
    public static Vector3 resourceB;
    public static Vector3 resourceC;

    public GameObject[] resourceObjects;

    void Start()
    {
        resourceA = resourceObjects[0].transform.position;
        resourceB = resourceObjects[1].transform.position;
        resourceC = resourceObjects[2].transform.position;

    }

}
