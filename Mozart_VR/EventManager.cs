using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR;

public class EventManager : MonoBehaviour
{
    // public SteamVR_Input_Sources handType;
    // public SteamVR_Behaviour_Pose controllerPose;
    // public SteamVR_Action_Boolean interAction;

    public GameObject Mozart;
    public VideoManager Screen;
    public DialogManager dialog;
    public Transform btn;
    bool chk = true;//나가기 버튼 눌렀을 시, true이면 다 본 것, false이면 아직 다 보지 않은 것

    public GameObject bgImage;//background image

    float imgAnim = 0.0f;

    public void onButton() {
        //버튼 색 변경
        ColorBlock cb = btn.GetComponent<Button>().colors;
        cb.normalColor = Color.green;

        if(btn.name == "Exit" && btn.parent.name != "Menu")
        {
            cb.normalColor = Color.red;
            ExitOption();
        }

        btn.GetComponent<Button>().colors = cb;
    }

    public void onButtonDown() {
        if(btn.name == "Exit")
        {
            //나가기 기능 구현
            StartCoroutine(WaitForEnd());
            return;
        }
        if(btn.parent.name == "Menu")
        {
            Screen.SendMessage(btn.name);
            return;
        }

        dialog.EndCurrentSentence();

        Screen.btn = btn;  
        string sub = btn.name.Substring(btn.name.Length-1);

        Screen.PlayVideo(int.Parse(sub));

        Mozart.GetComponent<Animator>().SetInteger("isYes", 0);
        Mozart.GetComponent<Animator>().SetBool("isFinish", false);
        Mozart.GetComponent<Animator>().SetInteger("Option", int.Parse(sub));

        btn.parent.gameObject.SetActive(false);
    }

    public void onButtonUp() {
        ColorBlock cb = btn.GetComponent<Button>().colors;
        cb.normalColor = Color.white;
        btn.GetComponent<Button>().colors = cb;
    }

    void ExitOption() {
        chk = true;
        foreach (bool item in Screen.isWatch)
        {
            if(!item)
                chk = false;
        }

        if(!chk)
        {
            dialog.DisplayCurrentSentence(Mozart.GetComponent<DialogTrigger>().dialog , 1);
            Mozart.GetComponent<Animator>().SetInteger("isYes", -1);
        }

        else
        {
            dialog.DisplayCurrentSentence(Mozart.GetComponent<DialogTrigger>().dialog , 0);
            Mozart.GetComponent<Animator>().SetInteger("isYes", 1);
        }
    }

    IEnumerator WaitForEnd() {
        bgImage.GetComponent<Animator>().SetTrigger("isFinish");

        yield return new WaitForSeconds(3f);

        ExitGame();

        yield break;
    }

    public void TheEnd() {
        bgImage.GetComponent<Animator>().SetTrigger("isFinish");
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit(); // 어플리케이션 종료
        #endif
    }

}
