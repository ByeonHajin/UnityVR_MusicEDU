using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource piano;

    public void StopAudio() {
        InvokeRepeating("FadeOut",1f,1f);
    }
    
    void FadeOut() {
        Debug.Log(piano.volume);
        if(piano.volume == 0)
        {
            piano.Stop();
            CancelInvoke("FadeOut");
        }
        
        piano.volume -= Time.deltaTime * 3f;
    }
    
}
