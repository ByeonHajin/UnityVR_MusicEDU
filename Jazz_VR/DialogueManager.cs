using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text nameText;
    public Text dialogueText;
    public Queue<string> sentences;
    private void Start() {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue) {
        dialogueBox.SetActive(true);

        sentences.Clear();
        nameText.text = dialogue.name;

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        // dialogueText.text = sentence;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

        Invoke("DisplayNextSentence", 4f);
    }

    IEnumerator TypeSentence(string sentence) {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue() {
        dialogueBox.SetActive(false);
    }

    public void RandomTalk(Dialogue dialogue) {
        int length = dialogue.sentences.Length;
        string sentence = dialogue.sentences[Random.Range(0,length)];

        dialogueBox.SetActive(true);
        nameText.text = dialogue.name;
        dialogueText.text = sentence;

        Invoke("EndDialogue", 2f);
    }
}
