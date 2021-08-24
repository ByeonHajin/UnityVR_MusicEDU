using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject dialogBox;
    public GameObject doorEffect;
    [SerializeField] private Queue<DialogData> sentences;
    public Text nameText;
    public Text context;
    public bool isEnd;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<DialogData>();
    }

    public void StartDialog(Dialog dialog) {
        dialogBox.SetActive(true);
        nameText.text =  dialog.name;

        sentences.Clear();

        foreach (DialogData dialogData in dialog.dialogs)
        {
            sentences.Enqueue(dialogData);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if(sentences.Count == 0)
        {
            EndTalk();
            return;
        }

        DialogData sentence = sentences.Dequeue();
        //context.text = sentence;
        
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence.sentence));
    }

    IEnumerator TypeSentence(string sentence) {
        context.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            context.text += letter;
            yield return null;
        }

        Invoke("DisplayNextSentence", 3f);
    }

    public void DisplayCurrentSentence(Dialog dialog, int idx) {
        nameText.text = dialog.name;

        context.text = dialog.dialogs[idx].sentence;

        dialogBox.SetActive(true);
    }

    public void EndCurrentSentence() {
        dialogBox.SetActive(false);
    }

    void EndTalk() {
        Debug.Log("End of Conversation");

        isEnd = true;
        dialogBox.SetActive(false);
        doorEffect.SetActive(true);
    }

    IEnumerator DoEvent(int eventNum)
    {
        Debug.Log(eventNum);
        yield break;
    }
}
