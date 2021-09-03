using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    // Script will oscillate a platform along the X, Y or Z axis.  By default, Allowed to Move is false. Enable in Inspector to allow movement on Update.

    // Can use the OscillatorTrigger script on a different object if you wish to enable Oscillation when another object is Triggered.



    Vector3 startPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range (0,1)] float movementFactor;
    [SerializeField] float period = 2f;
    
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

        const float tau = Mathf.PI * 2;  //   constant value. Mathf for games course recommended. Gamedev.TV
        float rawSineWave = Mathf.Sin(cycles * tau);  // going from -1 to 1

        movementFactor = (rawSineWave + 1f) / 2f; // recalculate to go from 0 to 1. cleaner easier to understand

        Vector3 offset = movementVector * movementFactor;
        transform.position = startPosition + offset;
    }
}
