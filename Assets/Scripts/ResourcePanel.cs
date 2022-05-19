using UnityEngine;

public class ResourcePanel : MonoBehaviour
{
    public GameObject resource;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            resource.SetActive(!resource.activeSelf);
    }
}
