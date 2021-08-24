using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBand : MonoBehaviour
{
    public Tutorial tutorial;
    Animator anim;
    // Update is called once per frame

    void Start() {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if(tutorial.isBand)
        {
            anim.SetBool("Play", true);
        }
        else
        {
            anim.SetBool("Play", false);
        }
    }
}
