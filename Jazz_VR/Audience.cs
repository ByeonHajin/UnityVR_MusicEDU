using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audience : MonoBehaviour
{
    public ButtonEvent btnEvent;
    Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }
    private void Update() {
        if(btnEvent.isPlay && !(btnEvent.isFinish))
            animator.SetBool("isPlay", true);
        else if(btnEvent.isPlay && btnEvent.isFinish)
            StartCoroutine(Applaud());


        if(btnEvent.isPlay)
            animator.SetBool("isPlay", true);
        else
        {
            if(animator.GetBool("isPlay"))
            {
                StartCoroutine(Applaud());
            }
        }

        if(btnEvent.isExit)
        {
            animator.SetBool("isPlay", true);
            StartCoroutine(Applaud());
        }
    }

    IEnumerator Applaud() {
        animator.SetTrigger("isApplaud");

        yield return new WaitForSeconds(5f);

        if(btnEvent.isExit)
            yield break;
        
        animator.ResetTrigger("isApplaud");
        btnEvent.isPlay = false;
        animator.SetBool("isPlay", false);
    }

}
