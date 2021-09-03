using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillatorTrigger : MonoBehaviour
{
 
 // This script will trigger the variable (Allowed to Move) within the Oscillator script to true. Allowing movement of the Oscillator object.
 // Attach this script to your  'Oscillator Trigger' object.  This will be your trigger or switch activator object.
 // In the Inspector for this object, find the  'Oscillating Object'  field  and  Assign your Oscillating object.

 // Attach the Oscillator script to your oscillating platform or object. 





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
