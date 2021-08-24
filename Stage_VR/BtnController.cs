using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnController : MonoBehaviour
{
    public void InteractBtn(Transform btn) {
        GameObject.Find("TutorialManager").GetComponent<Tutorial>().btn = btn;
        // tutorial.Btn = btn;
    }
}
