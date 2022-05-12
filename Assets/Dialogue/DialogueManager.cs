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
    private int count = 0;
    private GameObject enemy;
    private GameObject back1;
    private GameObject back2;
    private GameObject black;
    public Animator animator;

    private Queue<Tuple<string, string>> sentences;

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<Tuple<string, string>>();
        enemy = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
        back1 = GameObject.Find("BG").transform.GetChild(0).gameObject;
        back2 = GameObject.Find("BG").transform.GetChild(1).gameObject;
        black = GameObject.Find("BG").transform.GetChild(2).gameObject;
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

    private IEnumerator FirstChangeTransparent()
    {
        var firstMaterial = back1.GetComponent<Renderer>().material.color;
        for (; firstMaterial.a > 0;)
        {
            firstMaterial.a -= Time.deltaTime / 1000;
            back1.GetComponent<Renderer>().material.color = firstMaterial;
            yield return new WaitForSeconds(0.001f);
        }

        yield return null;
    }

    public void DisplayNextSentence()
    {
        switch (count)
        {
            case 12:
                FirstChangeTransparent();
                Debug.Log(back1.GetComponent<Renderer>().material.color.a);
                break;
            /*case 13:
                back2.SetActive(true);
                black.SetActive(false);
                break;*/
            case 16:
                enemy.SetActive(true);
                break;
        }

        count++;
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
        foreach (char letter in sentence)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        SceneManager.LoadScene("MainGame"); 
    }
}