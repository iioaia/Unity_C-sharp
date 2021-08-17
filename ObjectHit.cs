using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHit : MonoBehaviour
{


// This code script will cause a material color change on collision.

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Player")
        {
        GetComponent<MeshRenderer>().material.color = Color.cyan;
        gameObject.tag = "isHit";
        }
    }


}
