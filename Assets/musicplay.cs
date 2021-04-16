using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class musicplay : MonoBehaviour

{

    AudioSource piano;


    void Awake(){
        piano = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        piano.loop = true;
        piano.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
