using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    // public AudioSource Instructions;
    // public AudioClip[] audioClipArray;
    public AudioClip clip;
    public float volume=1;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(clip, transform.position, volume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    


}



