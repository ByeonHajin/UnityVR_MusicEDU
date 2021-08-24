using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public ButtonEvent btnEvent;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(btnEvent.isTalk)
        {
            StartCoroutine(WaitForTalk());
        }
    }

    IEnumerator WaitForTalk() {
        anim.SetBool("isTalk", true);

        yield return new WaitForSeconds(10f);

        anim.SetBool("isTalk", false);
        btnEvent.isTalk = false;
    }
}
