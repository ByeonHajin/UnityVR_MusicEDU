using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Auditorium : MonoBehaviour
{
    Image bgImage;
    float imgAnim = 0.0f;

    private void Awake() {
        GetComponent<Animator>().ResetTrigger("isFinish");
        GetComponent<Animator>().SetTrigger("isStart");
        bgImage = GetComponent<Image>();
        StartCoroutine(WaitForStart());
    }

    IEnumerator WaitForStart() {
        yield return new WaitForSeconds(3f);

        bgImage.color = new Color(0,0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
