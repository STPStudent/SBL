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

    [SerializeField] private GameObject enemy;
    [SerializeField] private Image introBg;
    [SerializeField] private Image blackBg;
    [SerializeField] private Image mainBg;

    public Animator animator;
    [SerializeField] private Image radianProgress;
    [SerializeField] private Image skipText;

    private Queue<Tuple<string, string>> sentences;
    private static readonly int IsOpenDialogue = Animator.StringToHash("IsOpenDialogue");

    private bool isChangeToBg2;
    private bool isChangeToBg3;

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<Tuple<string, string>>();
    }

    private void ChangeTransparency(Image image)
    {
        var newColor = new Color(image.color.r, image.color.g, image.color.b, image.color.a);
        newColor.a += 2 * Time.deltaTime;
        image.color = newColor;
    }

    private void Update()
    {
        if (isChangeToBg2)
            ChangeTransparency(blackBg);

        if (isChangeToBg3)
            ChangeTransparency(mainBg);

        if (Input.anyKey && !Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Escape))
        {
            skipText.fillAmount = 1;
            radianProgress.fillAmount += Time.deltaTime / 2f;
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
        animator.SetBool(IsOpenDialogue, true);
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
                isChangeToBg2 = true;
                break;
            case 9:
                isChangeToBg3 = true;
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
        animator.SetBool(IsOpenDialogue, false);
        SceneManager.LoadScene("MainGame");
    }
}