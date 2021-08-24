using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Band : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
    AudioSource testSource;
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        testSource = transform.parent.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameObject.Find("EventManager").GetComponent<ButtonEvent>().isExit)
        {
            if(audioSource.isPlaying || testSource.isPlaying)
                animator.SetBool("isPlay", true);
            else
                animator.SetBool("isPlay", false);
        }
        else
        {
            animator.SetBool("isPlay", false);
            animator.SetBool("isFinish", true);
        }
    }
}
