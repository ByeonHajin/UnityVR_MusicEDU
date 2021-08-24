using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    //각 이벤트에 따른 오브젝트 활성화
    [SerializeField]
    GameObject[] btns;
    public Transform btn;
    public AudioMixer audioMixer;
    public GameObject Flares;
    public GameObject btnText;
    public bool trigger = false;

    // public Camera VRcamera;

    public bool isFinish = false;
    public bool isBand = false;
    public bool isBtn;
    public bool isStart = false;
    
    //관객
    public Reaction mReact;

    RaycastHit hit;

    IEnumerator coroutine;
    LightController lightController;
    VideoController videoController;
    SoundController soundController;
    float waitTime;
    bool isAudience = false;

    void Start() {
        btnText.SetActive(false);

        lightController = GetComponent<LightController>();
        videoController = GetComponent<VideoController>();
        soundController = GetComponent<SoundController>();
    }

    public void btnClick() {
        if(btn != null)
        {
            if(btn.tag.Contains("Tutorial"))
            {
                StartCoroutine(SetTrigger());
            }
        }
        else
            trigger = false;
    }

    IEnumerator SetTrigger() {
        trigger = true;
        if(isAudience)
        {
            mReact.React(1);
        }
        yield return new WaitForSeconds(2f);

        trigger = false;
    }

    public void inactiveBtn() {
        isFinish = false;

        foreach (GameObject item in btns)
        {
            item.SetActive(false);
        }
    }

    public bool Tutorial1 () {
        if(!isBand)
        {
            Flares.SetActive(true);
            soundController.SoundPlay();
        }
        isBand = true;
        // trigger = false;
        for(int i=0;i<4;i++)
        {
            btns[i].SetActive(true);
        }

        btnClick();

        if(trigger && btn.tag == "Tutorial1")
        {
            soundController.SoundControl(btn.name);
        }
        return isFinish;
    }

    public bool Tutorial2 () {
        // trigger = false;

        for(int i=4;i<8;i++)
        {
            btns[i].SetActive(true);
        }

        btnClick();

        if(trigger && btn.tag == "Tutorial2")
            lightController.LightControl(btn.name);

        return isFinish;
    }

    public bool Tutorial3 () {
        // trigger = false;

        for(int i=8;i<12;i++)
        {
            btns[i].SetActive(true);
        }

        btnClick();

        if(trigger && btn.tag == "Tutorial3")
            videoController.VideoControl(int.Parse(btn.name));

        return isFinish;
    }

    public void goNext() {
        isFinish = true;
    }

    public void StopMusic() {
        soundController.SoundStop();
        isBand = false;
    }

    public void StartMusic() {
        soundController.SoundPlay();
        isBand = true;
    }

    public void WaitBtn() {
        if(trigger)
        {
            btnText.SetActive(false);
            waitTime = 0f;
            return;
        }

        waitTime += Time.deltaTime;

        if(waitTime > 15f)
        {
            btnText.SetActive(true);
            waitTime = 0f;
        }
    }

    public bool EndTutorial() {
        isAudience = true;
        Tutorial1();
        Tutorial2();
        Tutorial3();

        WaitBtn();

        if(!soundController.mSound.isPlaying)
        {
            isFinish = true;
            btnText.SetActive(false);
            return isFinish;
        }

        return isFinish;
    }

    public bool SetConsert() {
        Tutorial1();
        Tutorial2();
        Tutorial3();

        return isStart;
    }
}