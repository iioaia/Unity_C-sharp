using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    // PARAMETERS - variables
    // CACHE  e.g.  references
    // STATE - private instance member  variables
    

    [SerializeField] float thrustPower = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip engineThrust;

    [SerializeField] ParticleSystem thrustParticles;

    Rigidbody rb;
    AudioSource thrustSound;

   


 
    void Start()
    {
       rb = GetComponent<Rigidbody>();
       thrustSound = GetComponent<AudioSource>();
    }



    void Update()
    {
        ProcessThrust();
        ProcessRotation();

    }


// This will propel the rocket and play the sound of a thruster.
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            startThrusting();
        }

        else 
        {
            thrustSound.Stop();
            thrustParticles.Stop();
        }

        
    }



    void startThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustPower * Time.deltaTime);
        if (!thrustSound.isPlaying)
        {
            thrustSound.PlayOneShot(engineThrust);
        }

        if (!thrustParticles.isPlaying)
        {
            thrustParticles.Play();
        }
    }


// Enables the rocket to rotate.

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
        
        }

        else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
        }

    }



    void ApplyRotation(float rotationThisFrame)
    {
        
        rb.freezeRotation = true; // freezing physics rotation to manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;  // unfreeze physics rotation, permit physics system to take over.
    }




}
