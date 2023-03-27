using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpBox : MonoBehaviour
{
    public GameObject helpBox;

    // Start is called before the first frame update
    void Start()
    {
        helpBox.SetActive(false);
    }

    public void activateBox() 
    {
        helpBox.SetActive(true);
    }

    public void deactivateBox() 
    {
        helpBox.SetActive(false);
    }
}
