using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip success;
    [SerializeField] float sceneLoadDelay = 1.8f;

    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] ParticleSystem successParticles;
   

    AudioSource sounds;



    bool isTransitioning = false; // use in an IF statement
    bool collisionDisabled = false; 



    void Start()
    {
        sounds = GetComponent<AudioSource>();
    
    }


    void Update()
    {
      RespondToDebugKeys();

    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextScene();
        }

        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; // this will toggle collision 
        }


    }




    void OnCollisionEnter(Collision other) 
    {


        if (isTransitioning || collisionDisabled ) {return;}   // if it is transitioning OR collisions are disabled, return, meaning, DO nothing below.
    
        

        // Perform an action IF or Else one of these conditions are met
        switch(other.gameObject.tag)
        {
            case "Friendly":
                
                break;
            case "Finish":

                SuccessLoadNextScene();
                break;
            case "Fuel":
                // Placeholder, not used at the moment.
                break;
            default:
                StartCrashSequence();
                break;
        }

    }




    void SuccessLoadNextScene()
    {
    
        isTransitioning = true;
        sounds.Stop();
        sounds.PlayOneShot(success);
        successParticles.Play();
        GetComponent<RocketMovement>().enabled = false;  // prevent controls if Success
        Invoke("LoadNextScene", sceneLoadDelay);
    }




    void StartCrashSequence()
    {
        isTransitioning = true;
        sounds.Stop();
        sounds.PlayOneShot(explosion);
        explosionParticles.Play();
        GetComponent<RocketMovement>().enabled = false; //  prevent controls if Crashed
        Invoke("ReloadScene", sceneLoadDelay);
    }



// Here we Reload the scene if the rocket crashes into a default(tag) object.
    void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;  // creates an Interger variable which contains the value of the Active scene 0,1,2,3,4 etc. (based on scene order in Build Settings)
        SceneManager.LoadScene(currentSceneIndex);
    }



    void LoadNextScene() // Loads next scene in the build settings order, 0,1,2,3,4 etc.
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // gets the active scene
        int nextSceneIndex = currentSceneIndex + 1;                       // Variable that will Add 1 to the active scene number  eg:  scene 0 + 1 = scene 1
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)    // counts the quantity of scenes in build settings  and compares it to the variable nextSceneIndex
        {
            nextSceneIndex = 0;    //
        }
        SceneManager.LoadScene(nextSceneIndex);  // Load beginning scene 0
    
    }

}
