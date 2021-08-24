using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaction : MonoBehaviour
{
    public AudioClip[] clips;//0:처음 함성, 1:버튼 클릭시 함성
    public Tutorial tutorial;
    AudioSource mAudio;
    // Start is called before the first frame update
    void Start()
    {
        mAudio = GetComponent<AudioSource>();

        mAudio.clip = clips[0];//처음 함성
        mAudio.Play();
    }
    void Update() {
        // if(tutorial.trigger)
        // {
        //     mAudio.clip = clips[1];
        //     mAudio.Play();
        // }
        if(tutorial.isFinish)
        {
            mAudio.clip = clips[2];
            mAudio.Play();
        }
    }

    public void React(int idx) {
        mAudio.clip = clips[idx];
        mAudio.Play();
    }
}
