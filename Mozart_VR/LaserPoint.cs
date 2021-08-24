using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class LaserPoint : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean interAction;

    public GameObject laserPrefab;
    private GameObject laser;
    private Transform laserTransform;
    private Vector3 hitPoint;

    public EventManager eventManager;
    public AudioManager audioManager;

    bool trigger = true;


    void Start() {
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
    }

    // Update is called once per frame
    void Update()
    {
        isBtn();
    }

    bool isBtn() {
        RaycastHit hit = new RaycastHit();

        if(Physics.Raycast(controllerPose.transform.position, transform.forward, out hit))
        {
            hitPoint = hit.point;
            if(hit.collider.tag == "Door" && FindObjectOfType<DialogManager>().isEnd)
            {
                ShowLaser(hit);
                if(interAction.GetState(handType))
                {
                    StartCoroutine(WaitForEnd());
                }
            }

            if(hit.collider.tag == "Button")
            {
                ShowLaser(hit);
                eventManager.btn = hit.transform;

                eventManager.onButton();
                if(interAction.GetState(handType) && trigger)
                {
                    eventManager.onButtonDown();
                    StartCoroutine(WaitTrigger());
                }
            }
            else
            {
                laser.SetActive(false);
                if(eventManager.btn!=null)
                {
                    eventManager.onButtonUp();
                }
                return false;
            }
        }

        return false;
    }

    IEnumerator WaitForEnd() {
        eventManager.TheEnd();
        audioManager.StopAudio();

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("Auditorium");
    }

    IEnumerator WaitTrigger() 
    {
        trigger = false;

        yield return new WaitForSeconds(1f);

        trigger = true;
    }

    private void ShowLaser(RaycastHit hit) {
        laser.SetActive(true);
        laser.transform.position = Vector3.Lerp(controllerPose.transform.position, hitPoint, 0.5f);
        laserTransform.LookAt(hitPoint);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, hit.distance);
    }
}
