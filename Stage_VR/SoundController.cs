using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    public AudioSource mSound;
    public AudioClip[] clips;
    public AudioMixer mMixer;
    bool chk =false;
    int ran = 2;

    private void Start() {
    }

    public void SoundPlay() {
        if(!chk)
        {
            ran = Random.Range(0,2);
        }
        chk = true;
        mSound.clip = clips[ran];
        mSound.Play();
    }

    public void SoundStop() {
        mSound.Stop();
    }

    public void SoundControl(string btnName) {
        if(btnName == "Reset")
        {
            mMixer.SetFloat("High", 1.0f);
            mMixer.SetFloat("Low", 1.0f);
            mMixer.SetFloat("Middle", 1.0f);
        }
        else
            mMixer.SetFloat(btnName, 2.0f);
    }
}
