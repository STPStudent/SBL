using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    private int count;
    private GameObject enemy;
    private GameObject introBg;
    private GameObject mainBg;
    private GameObject blackBg;
    public Animator animator;
    [SerializeField] private Image radianProgress;
    [SerializeField] private Image skipText;
    private Queue<Tuple<string, string>> sentences;
    private static readonly int IsOpen = Animator.StringToHash("IsOpen");

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<Tuple<string, string>>();
        enemy = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
        introBg = GameObject.Find("BG").transform.GetChild(0).gameObject;
        mainBg = GameObject.Find("BG").transform.GetChild(1).gameObject;
        blackBg = GameObject.Find("BG").transform.GetChild(2).gameObject;
    }

    private void Update()
    {
        if (Input.anyKey && !Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Escape))
        {
            skipText.fillAmount = 1;
            radianProgress.fillAmount += Time.deltaTime / 4f;
            if (radianProgress.fillAmount >= 1f)
                EndDialogue();
        }
        else
        {
            radianProgress.fillAmount = 0;
            skipText.fillAmount = 0;
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool(IsOpen, true);
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
        switch (count)
        {
            case 8:
                introBg.SetActive(false);
                blackBg.SetActive(true);
                break;
            case 9:
                mainBg.SetActive(true);
                blackBg.SetActive(false);
                break;
            case 12:
                enemy.SetActive(true);
                break;
        }

        count++;
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        var (item1, sentence) = sentences.Dequeue();
        nameText.text = item1;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (var letter in sentence)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.013f);
        }
    }

    private void EndDialogue()
    {
        animator.SetBool(IsOpen, false);
        SceneManager.LoadScene("MainGame"); 
    }
}