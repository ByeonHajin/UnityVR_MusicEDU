using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class LaserPointer : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean interAction;

    public GameObject laserPrefab;
    private GameObject laser;
    private Transform laserTransform;
    private Vector3 hitPoint;

    public Transform btn;
    public Tutorial tutorial;

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
            if(hit.collider.name == "Next" || hit.collider.name == "Start" || hit.collider.name == "Finish")
            {
                ShowLaser(hit);
                btn = hit.transform;

                onButton();
                if(interAction.GetState(handType) && trigger)
                {
                    onButtonDown();
                    StartCoroutine(WaitTrigger());
                }
            }
            else
            {
                laser.SetActive(false);
                if(btn!=null)
                {
                    onButtonUp();
                }
                return false;
            }
        }

        return false;
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

    void onButton()
    {
        ColorBlock cb = btn.GetComponent<Button>().colors;
        cb.normalColor = Color.green;

        btn.GetComponent<Button>().colors = cb;
    }

    void onButtonDown()
    {
        if(btn.name == "Next" || btn.name == "Finish")
            tutorial.goNext();
        else
            tutorial.isStart = true;
    }

    void onButtonUp()
    {
        ColorBlock cb = btn.GetComponent<Button>().colors;
        cb.normalColor = Color.white;
        btn.GetComponent<Button>().colors = cb;
    }
}
