using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ButtonEvent : MonoBehaviour
{
    public GameObject rhythmPanel;
    public GameObject BassPanel;
    public GameObject PianoPanel;
    public GameObject FinishPanel;

    public Transform btn;
    public AudioSource mAudio;
    public AudioSource DrumAudio;
    public AudioSource BassAudio;
    public AudioSource PianoAudio;
    public AudioSource soundEffect;
    public AudioClip[] clips;
    public AudioClip[] sounds;
    public Image bgImage;//background image
    public bool isTalk;
    public bool isPlay = false;
    public bool isFinish;
    public bool isExit = false;

    Transform rhythm;
    int idx;
    float timer;
    float imgAnim = 0.0f;
    float pianoTimer = 0.0f;
    
    void TestButton() {
        btn.GetComponent<DialogueTrigger>().TriggerDialogue();
        isTalk = true;

        int ran = Random.Range(0,2);
        mAudio.clip = clips[ran];

        StartCoroutine(WaitForEndOfSong());
    }

    IEnumerator WaitForEndOfSong() {
        mAudio.Play();
        isPlay = true;

        yield return new WaitUntil(() => (mAudio.isPlaying == false)||Input.GetMouseButton(1));

        /////Test용/////
        if(Input.GetMouseButton(1))
            mAudio.Stop();
        ///////////////
        isPlay = false;
        soundEffect.clip = sounds[0];
        soundEffect.Play();

        mAudio.GetComponent<DialogueTrigger>().TriggerDialogue();
        
        rhythmPanel.SetActive(true);
    }

    void Rhythm() {
        DrumAudio.clip = btn.GetComponent<MusicOption>().jazz.Drum;
        DrumAudio.Play();
    }
    void Bass() {
        int idx;
        if(btn.name.Contains("1")) idx = 0;
        else idx = 1;

        BassAudio.clip = rhythm.GetComponent<MusicOption>().jazz.Basses[idx].clip;
        BassAudio.Play();

    }
    void Piano() {
        int idx;
        if(btn.name.Contains("1")) idx = 0;
        else idx = 1;

        PianoAudio.clip = rhythm.GetComponent<MusicOption>().jazz.Pianos[idx].clip;
        PianoAudio.Play();
    }

    public void Preview(int idx, AudioClip clip) {
        AudioSource audioSource;

        switch (idx)
        {
            case 1:
                audioSource = DrumAudio;
                break;
            case 2:
                audioSource = BassAudio;
                break;
            case 3:
                audioSource = PianoAudio;
                break;
            default:
                return;
        }

        audioSource.clip = clip;
        audioSource.Play();
    }

    public void ExitPreview(int idx) {
        AudioSource audioSource;

        switch (idx)
        {
            case 1:
                audioSource = DrumAudio;
                break;
            case 2:
                audioSource = BassAudio;
                break;
            case 3:
                audioSource = PianoAudio;
                break;
            default:
                return;
        }

        audioSource.Stop();
    }

    void Bossa() {
        GetDrum();
    }
    void Boogie() {
        GetDrum();
    }
    void Swing() {
        GetDrum();
    }

    IEnumerator activePanel(GameObject Panel) {
        yield return new WaitForSeconds(1f);
        if(Panel != rhythmPanel)
            Panel.transform.GetChild(3).gameObject.SetActive(true);
        Panel.SetActive(true);
    }

    void GetDrum() {
        rhythm = btn;
        DrumAudio.clip = btn.GetComponent<MusicOption>().jazz.Drum;
        DrumAudio.loop = true;
        DrumAudio.Play();

        idx = 1;
        BassPanel.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = btn.GetComponent<MusicOption>().jazz.Basses[0].name;
        BassPanel.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = btn.GetComponent<MusicOption>().jazz.Basses[1].name;
        // BassPanel.SetActive(true);
        StartCoroutine(activePanel(BassPanel));
    }

    void Bass1() {
        GetBass(1);
    }
    void Bass2() {
        GetBass(2);
    }

    void GetBass(int num) {
        BassAudio.clip = rhythm.GetComponent<MusicOption>().jazz.Basses[num-1].clip;
        BassAudio.loop = true;
        BassAudio.Play();

        idx = 2;
        PianoPanel.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = rhythm.GetComponent<MusicOption>().jazz.Pianos[0].name;
        PianoPanel.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = rhythm.GetComponent<MusicOption>().jazz.Pianos[1].name;
        // PianoPanel.SetActive(true);
        StartCoroutine(activePanel(PianoPanel));
    }

    void Piano1() {
        GetPiano(1);
    }
    void Piano2() {
        GetPiano(2);
    }

    void GetPiano(int num) {
        PianoAudio.clip = rhythm.GetComponent<MusicOption>().jazz.Pianos[num-1].clip;
        PianoAudio.Play();

        isPlay = true;
        idx = 3;
        FinishPanel.SetActive(true);
        StartCoroutine(WaitForFinishBand());
    }


    IEnumerator WaitForFinishBand() {
        yield return new WaitUntil(() => ((pianoTimer += Time.deltaTime) > PianoAudio.clip.length - 3f));

        isPlay = false; //Audience 박수
        soundEffect.clip = sounds[1];
        soundEffect.Play();
        InvokeRepeating("FadeOut",1f,1f);

        yield return new WaitUntil(() => !PianoAudio.isPlaying);
        
        bgImage.enabled = true;
        yield return new WaitForSeconds(5f);//밴드가 인사하고 관객이 환호
        
        yield return new WaitUntil(() => TheEnd() == true);

        ExitGame();

        yield break;
    }

    void Back() {
        switch (idx)
        {
            case 1:
                StartCoroutine(activePanel(rhythmPanel));
                break;
            case 2:
                idx = 1;
                StartCoroutine(activePanel(BassPanel));
                break;
            case 3:
                idx = 2;
                StartCoroutine(activePanel(PianoPanel));
                break;
        }
    }

    void Finish() {
        pianoTimer = PianoAudio.clip.length - 3f;
    }

    void FadeOut() {
        Debug.Log(DrumAudio.volume);

        if(DrumAudio.volume == 0 || BassAudio.volume ==0)
        {
            isExit = true;//Band 인사 애니메이션 재생
            PianoAudio.Stop();
            DrumAudio.Stop();
            BassAudio.Stop();
            CancelInvoke("FadeOut");
        }

        PianoAudio.volume -= Time.deltaTime * 30f;
        BassAudio.volume -= Time.deltaTime * 30f;
        DrumAudio.volume -= Time.deltaTime * 30f;
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
