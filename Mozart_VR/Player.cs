using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public EventManager eventManager;
    public DialogManager manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if(other.name == "Seat")
        {
            Debug.Log("Seat");
            eventManager.Mozart.GetComponent<DialogTrigger>().TriggerDialog();
            eventManager.Mozart.GetComponent<Animator>().SetBool("isTalk", true);
            Destroy(other.gameObject);

            StartCoroutine(WaitEndTalk());
        }
    }

    IEnumerator WaitEndTalk() {

        yield return new WaitUntil(() => manager.isEnd);

        eventManager.Mozart.GetComponent<Animator>().SetBool("isTalk", false);
    }

}
