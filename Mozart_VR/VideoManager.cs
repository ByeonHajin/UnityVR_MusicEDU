using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoManager : MonoBehaviour
{
    public GameObject Mozart;
    public GameObject Screen;
    public GameObject Menu;
    public VideoClip[] videos;
    public Transform btn;
    public GameObject SubtitleBox;
    public Text context;
    // public Text pauseText;
    public bool[] isWatch = {false, false, false};

    public Image pauseImage; //기존에 존재하는 이미지
    public Sprite TestSprite; //바뀌어질 이미지
    public Sprite pauseSprite; //기존 정지 이미지

    Animator subtitleAnim;
    bool isPause = false;
    float videoSpeed;

    [SerializeField] private Queue<DialogData> sentences;
    VideoPlayer videoPlayer;

    private void Start() {
        videoPlayer = Screen.GetComponent<VideoPlayer>();
        sentences = new Queue<DialogData>();
        subtitleAnim = Mozart.GetComponent<Animator>();
    }
    public void PlayVideo(int num) 
    {
        videoPlayer.clip = videos[num-1];
        isWatch[num-1] = true;

        videoPlayer.Play();
        Menu.SetActive(true);
        StartSubtitle(btn.GetComponent<DialogTrigger>().dialog);
    }

    void StartSubtitle(Dialog dialog) {
        Debug.Log("Success");

        sentences.Clear();

        foreach (DialogData dialogData in dialog.dialogs)
        {
            sentences.Enqueue(dialogData);
        }
    }

    public void DisplayNextSentence() {
        if(sentences.Count == 0)
        {
            EndTalk();
            return;
        }
        
        SubtitleBox.SetActive(true);
        DialogData sentence = sentences.Dequeue();
        //context.text = sentence;
        
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence.sentence));
    }

    IEnumerator TypeSentence(string sentence) {
        context.text = "";
        int i=0;
        foreach (char letter in sentence.ToCharArray())
        {
            i++;
            context.text += letter;
            yield return null;
        }

        yield return new WaitUntil(() => i >= sentence.Length);
        yield return new WaitForSeconds(10f);

        SubtitleBox.SetActive(false);
    }

    void EndTalk() {
        SubtitleBox.SetActive(false);
        subtitleAnim.SetBool("isFinish", true);
        Menu.SetActive(false);
        isPause = false;

        btn.parent.gameObject.SetActive(true);
    }

    void Pause() {
        isPause = !isPause;

        if(isPause)
        {
            videoPlayer.Pause();
            videoSpeed = subtitleAnim.speed;
            subtitleAnim.speed = 0.0f;
            // pauseText.text = "재생";
            pauseImage.sprite = TestSprite;
        }

        if(!isPause)
        {
            videoPlayer.Play();
            subtitleAnim.speed = videoSpeed;
            // pauseText.text = "일시\n정지";

            pauseImage.sprite = pauseSprite;
        }
    }

    void ToOption() 
    {
        videoPlayer.Stop();
        subtitleAnim.SetBool("isFinish", true);
        SubtitleBox.SetActive(false);
        pauseImage.sprite = pauseSprite;
        Menu.SetActive(false);

        btn.parent.gameObject.SetActive(true);
    }
}
