using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    private static bool sceneEnd;
    public float fadeSpeed = 1.5f;
    public int nextLevel;
    private Image image;
    private bool sceneStarting;

    void Awake()
    {
        image = GetComponent<Image>();
        image.enabled = true;
        sceneStarting = true;
        sceneEnd = false;
        Cursor.visible = false;
    }

    void Update()
    {
        if (sceneStarting) StartScene();
        if (sceneEnd) EndScene();
    }

    void StartScene()
    {
        image.color = Color.Lerp(image.color, Color.clear, fadeSpeed * Time.deltaTime);

        if (image.color.a <= 0.01f)
        {
            image.color = Color.clear;
            image.enabled = false;
            sceneStarting = false;
            Cursor.visible = true;
        }
    }

    void EndScene()
    {
        image.enabled = true;
        image.color = Color.Lerp(image.color, Color.black, fadeSpeed * Time.deltaTime);

        if (image.color.a >= 0.95f)
        {
            Cursor.visible = false;
            image.color = Color.black;
            Application.LoadLevel(nextLevel);
        }
    }
}