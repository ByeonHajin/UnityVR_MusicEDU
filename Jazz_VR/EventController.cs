using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;


public class EventController : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean interAction;

    public ButtonEvent btnEvent;
    Transform btn;
    MusicOption Band;
    bool trigger = true;
    int BandIdx;

    public GameObject laserPrefab;
    private GameObject laser;
    private Transform laserTransform;
    private Vector3 hitPoint;

    Color prevColor;

    void Start() {
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
    }

    private void FixedUpdate() {
        isBtn();
    }

    //VR에서 버튼 이벤트를 감지하는 스크립트, 레이캐스트와 콜라이더를 활용하여 버튼 이벤트 감지
    bool isBtn() {
        RaycastHit hit = new RaycastHit();

        if(Physics.Raycast(controllerPose.transform.position, transform.forward, out hit))
        {
            hitPoint = hit.point;
            if(hit.collider.tag == "Button")
            {
                ShowLaser(hit);
                btn = hit.transform;

                onButton();
                if(interAction.GetState(handType))
                {
                    onButtonDown();
                }
            }
            else
            {
                laser.SetActive(false);
                if(btn!=null)
                {
                    trigger = true;
                    onButtonUp();
                }
                trigger = true;
                return false;
            }
        }

        return false;
    }

    private void ShowLaser(RaycastHit hit) {
        laser.SetActive(true);
        laser.transform.position = Vector3.Lerp(controllerPose.transform.position, hitPoint, 0.5f);
        laserTransform.LookAt(hitPoint);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, hit.distance);
    }
    
    void onButton() {
        ColorBlock cb = btn.GetComponent<Button>().colors;
        cb.normalColor = Color.green;

        if(btn.name.Contains("Finish"))
        {
            cb.normalColor = Color.red;
            btn.GetComponent<Button>().colors = cb;
            return;
        }

        btn.GetComponent<Button>().colors = cb;

        if(btn.name.Contains("Test") || btn.name.Contains("Back"))
            return;

        if(!trigger)
            return;
        
        if(btn.parent.name == "Rhythm")
        {
            Band = btn.GetComponent<MusicOption>();
            btnEvent.Preview(1, Band.jazz.Drum);
            BandIdx = 1;
        }
        
        if(btn.parent.name == "Bass"|| btn.parent.name=="Piano")
        {
            int num;
            string sub = btn.name.Substring(btn.name.Length-1);
            num = int.Parse(sub);
            if(btn.parent.name == "Bass")
            {
                btnEvent.Preview(2, Band.jazz.Basses[num-1].clip);
                BandIdx = 2;
            }
            else
            {
                btnEvent.Preview(3, Band.jazz.Pianos[num-1].clip);
                BandIdx = 3;
            }
        }
        
        trigger = false;
    }

    void onButtonDown() {
        BandIdx = -1;

        btnEvent.btn = btn;
        btnEvent.SendMessage(btn.name);
        
        btn.parent.gameObject.SetActive(false);
    }

    void onButtonUp() {
        ColorBlock cb = btn.GetComponent<Button>().colors;
        cb.normalColor = Color.white;
        btn.GetComponent<Button>().colors = cb;

        if(btn.name.Contains("Test")|| btn.name.Contains("Back"))
            return;
        
        if(BandIdx == -1)
            return;

        btnEvent.ExitPreview(BandIdx);
    }
}
