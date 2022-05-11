using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    private Queue<Tuple<string, string>> sentences;

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<Tuple<string, string>>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        

        sentences.Clear();

        foreach (var sentence in dialogue.sentencesList)
        {
            var sentenceSplit = sentence.Split('#');
            nameText.text = sentenceSplit[0];
            sentences.Enqueue(new Tuple<string, string>(sentenceSplit[0], sentenceSplit[1]));
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        var dequeue = sentences.Dequeue();
        string sentence = dequeue.Item2;
        nameText.text = dequeue.Item1;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds((float)0.02);
        }
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        SceneManager.LoadScene("MainGame"); 
    }
}