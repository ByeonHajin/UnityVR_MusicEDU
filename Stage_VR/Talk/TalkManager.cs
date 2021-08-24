using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    public GameObject Dialog;
    public Animator anim;
    public Text nameText;
    public Text context;
    public GameObject startUI;

    public GameObject nextUI;
    public GameObject finishUI;
    public Tutorial tutorial;
    public GameObject audiences;
    public GameObject director;
    public Image bgImage;//background image

    public bool endMusic; //노래 끝 체크하는 함수
    public bool isBand = false;
    float imgAnim = 0.0f;

    [SerializeField] private Queue<TalkData> sentences; //text

    void activeObject(int num) {//active할 오브젝트의 인덱스
        GetComponent<Object>().active(num);
    }

    private void Start() {
        sentences = new Queue<TalkData>();
    }

    public void StartTalk(TalkContainer talk) {
        // anim.SetBool("isOpen", true);
        Dialog.SetActive(true);
        nameText.text = talk.name;

        sentences.Clear();

        // // foreach (TalkContent eachTalk in talk.sentences)
        foreach (TalkData talkData in talk.talkDatas)
        {
            sentences.Enqueue(talkData);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if(sentences.Count == 0)
        {
            EndTalk();
            nextUI.SetActive(false);
            StartCoroutine(StartConcert());
            return;
        }
        TalkData sentence = sentences.Dequeue();
        //context.text = sentence;
        
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence.sentence));

        if(sentence.isEvent != 0)
        {
            StartCoroutine(DoEvent(sentence.isEvent));   
        }
        else
            Invoke("DisplayNextSentence", 3f);
    }

    IEnumerator TypeSentence(string sentence) {
        context.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            context.text += letter;
            yield return null;
        }
    }

    void EndTalk() {
        // anim.SetBool("isOpen", false);
        Dialog.SetActive(false);
        Debug.Log("End of Conversation");
    }

    IEnumerator DoEvent(int eventNum) {
        if(eventNum == 1)
        {
            activeObject(eventNum);
            nextUI.SetActive(true);//튜토리얼 시작 시 next버튼 활성화, 튜토리얼이 모두 완료가 되면 비활성화
        }
        if(eventNum == 2 || eventNum == 3)
        {
            director.GetComponent<Animator>().SetBool("isClap", true);
            director.GetComponent<Director>().DoClap();//Audio
        }
        yield return new WaitForSeconds(3.5f);

        director.GetComponent<Animator>().SetBool("isClap", false);
        EndTalk();

        yield return new WaitForSeconds(1f);

        string Func = "Tutorial" + eventNum.ToString();
        
        yield return new WaitUntil(() => tutorial.GetType().GetMethod(Func).Invoke(tutorial,null).Equals(true));

        tutorial.isFinish = false;
        tutorial.inactiveBtn();

        // anim.SetBool("isOpen", true);
        Dialog.SetActive(true);
        DisplayNextSentence();
    }

    IEnumerator StartConcert() {
        startUI.SetActive(true);

        // yield return new WaitUntil(() => tutorial.isStart == true); // 무대세팅 완료
        yield return new WaitUntil(() => tutorial.SetConsert() == true);

        startUI.SetActive(false);
        tutorial.isFinish = false;
        tutorial.StopMusic();
        audiences.SetActive(true); // 관객 입장
        isBand = true;

        yield return new WaitForSeconds(5f);

        tutorial.StartMusic();
        finishUI.SetActive(true);

        yield return new WaitUntil(() => tutorial.EndTutorial() == true);

        finishUI.SetActive(false);
        Debug.Log("Finish");
        bgImage.enabled = true;
        
        yield return new WaitUntil(() => TheEnd() == true);

        ExitGame();
        yield break;
    }

    bool TheEnd() {
        imgAnim += Time.deltaTime;
        bgImage.color = Color.Lerp(new Color(0,0,0,0), Color.black, imgAnim);

        if(bgImage.color == Color.black)
            return true;
        
        return false;
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
