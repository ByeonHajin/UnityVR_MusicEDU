using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Director : MonoBehaviour
{
    [SerializeField] NavMeshAgent nav;
    [SerializeField] Animator dirAnim;
    public Transform dest;
    public Transform target;//Player
    float animSpeed = 0f;
    bool isTrigger = false;
    public TalkManager talk;
    public Transform Band;
    public AudioClip[] footSteps;
    public AudioClip clap;
    AudioSource mAudio;
    
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        dirAnim = GetComponent<Animator>();
        mAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(talk.isBand)
        {
            transform.LookAt(Band.position);
        }
        else
        {
            move();
        }
    }

    void move() {
        nav.SetDestination(dest.position);

        if(nav.velocity.sqrMagnitude > 0.1f * 0.1f && nav.remainingDistance <= 0.2f) {
            // transform.LookAt(target);

            Vector3 relativePos = target.position - transform.position;

            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;

            nav.isStopped = true;
            if(!isTrigger)
            {
                GetComponent<TalkTrigger>().TriggerTalk();
                isTrigger = true;
            }
        }
        dirAnim.SetFloat("Speed", nav.velocity.sqrMagnitude);
    }

    public void FootStep(int idx) {
        mAudio.clip = footSteps[idx];
        mAudio.Play();
    }

    public void DoClap() {
        mAudio.clip = clap;
        mAudio.Play();
    }
}
