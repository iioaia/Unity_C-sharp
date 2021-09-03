using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    // Script will oscillate an object along the X, Y or Z axis.  

    // Attach this script to your Oscillating object.
    // In this object's Inspector: 
    ///   By default, 'Allowed To Move'  is false. 
    //     Enable 'Allowed To Move' in the Inspector to allow movement without Trigger.  
    ///     Find the field  'Oscillator Trigger'  and assign the appropriate Oscillator Trigger for this object.





    Vector3 startPosition;
    [SerializeField] Vector3 movementVector;  // X = left/right,    Y = up/down,   Z = depth in/out   Higher = Farther out before returning
    [SerializeField] [Range (0,1)] float movementFactor;
    [SerializeField] float period = 2f;  // The higher this is  the slower it will move
    
    public bool allowedToMove = false;
    public GameObject oscillatorTrigger;  // Used only if an Oscillator Trigger object is used.



    void Start()
    {
        startPosition = transform.position;
        
        
    }





    // Update is called once per frame
    void Update()
    {
        
        if (period <= Mathf.Epsilon) {return;}

        if (allowedToMove != false)
        {
            BeginOscillation();
            return;
        }
 

    }

    private void BeginOscillation()
    {
        float cycles = Time.time / period; // continually growing over time

        const float tau = Mathf.PI * 2;  //   constant value.
        float rawSineWave = Mathf.Sin(cycles * tau);  // going from -1 to 1

        movementFactor = (rawSineWave + 1f) / 2f; // recalculate to go from 0 to 1. cleaner easier to understand

        Vector3 offset = movementVector * movementFactor;
        transform.position = startPosition + offset;
    }
}
