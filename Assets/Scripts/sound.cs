using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class sound : MonoBehaviour
{
    
    AudioSource snapSound;


   


    void Awake () {

        snapSound = GetComponent<AudioSource>();
    }


    void OnCollisionEnter (Collision coll)
    {
       snapSound.Play();
    }
}
