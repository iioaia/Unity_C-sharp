using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{   

//  When attached to a game object, in the Inspector you can determine which angle and at what speed the object will rotate.

    [SerializeField] float xAngle = 0.0f;
    [SerializeField] float yAngle = 0.0f;
    [SerializeField] float zAngle = 0.0f;



    void Update()
    {
        transform.Rotate(xAngle,yAngle,zAngle);
    }
}
