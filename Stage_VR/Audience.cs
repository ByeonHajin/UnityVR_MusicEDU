using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Audience : MonoBehaviour
{
    NavMeshAgent nav;
    Transform dest;
    Animator animator;
    public Transform band;
    public Tutorial tutorial;
    bool isReact = false;
    //Animator audience;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnEnable() {
        nav = transform.GetChild(0).GetComponent<NavMeshAgent>();
        dest = transform.GetChild(1);
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update() {
        if(!nav.isStopped)
            move();
        else
        {
            StartCoroutine(React());
        }
    }

    public void move() {
        nav.SetDestination(dest.position);
        if(nav.velocity.sqrMagnitude > 0.1f * 0.1f && nav.remainingDistance <= 0.2f) {
            animator.SetTrigger("MusicPlay");
            transform.GetChild(0).LookAt(band);
            nav.isStopped = true;
        }
    }

    IEnumerator React() {
        if(!tutorial.trigger)
        {
            // animator.SetBool("isReact", false);
            animator.SetInteger("isPlay", 0);
            yield break;
        }
        // animator.SetBool("isReact", true);
        // animator.SetInteger("isPlay", 0);
        int ran = Random.Range(1,5);

        animator.SetInteger("isPlay", ran);
        //yield return new WaitUntil(() => !tutorial.trigger);
    }
}
