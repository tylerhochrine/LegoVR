using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
 
public class footstep : MonoBehaviour
{
    AudioSource foot;
    private bool IsMoving;
 
    void Start()
    {
      
    }

    void Awake () {

        foot = GetComponent<AudioSource>();
    }
 
    void Update()
    {
        if (Input.GetAxis("Vertical") < 0) IsMoving = true; // better use != 0 here for both directions
        else IsMoving = false;
 
        if (IsMoving && !foot.isPlaying) foot.Play(); // if player is moving and audiosource is not playing play it
        if (!IsMoving) foot.Stop(); // if player is not moving and audiosource is playing stop it

    }

    void OnCollisionEnter(Collision collision)
    {
        //foot = gameObject.GetComponent<AudioSource>();
        foot.Play();
    }
} 

     /**
    void mus() {
          if(trigger.GetComponent.<Collider>().tag=="floor")
          {
              foot.play();
        
          }
    }
    **/
