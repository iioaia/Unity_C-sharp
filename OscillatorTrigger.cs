using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillatorWAIT : MonoBehaviour
{
 
 // This script will Enable the Allowed to Move  variable within the Oscillator script to true.  Allowing movement of the specified Oscillator platform.
 // It can also be used as an activator switch. 
 // Attach this script to your activator (trigger) object.  Assign the Oscillator object in the inspector.
 // Attach the Oscillator script to your moving, oscillating platform or object.  Assign it an activator (trigger) object in the inspector.



    public Oscillator OscillatingObject;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            OscillatingObject.allowedToMove = true;

        }
    }




    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //allowedToMove = false;   
            return;
        }
    }

















}
