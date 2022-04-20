using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResoursePanel : MonoBehaviour
{
    public GameObject resourse;
    public bool isResourse = true;
    // Start is called before the first frame update
    void Start()
    {
        resourse = GameObject.Find("Resourse");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isResourse = !isResourse;
            resourse.SetActive(!resourse.activeSelf);

        }
    }
}
