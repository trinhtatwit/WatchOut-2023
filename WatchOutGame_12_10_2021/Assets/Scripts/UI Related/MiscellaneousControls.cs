using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiscellaneousControls : MonoBehaviour
{
    public PlayerEntersRing redRings;
    public GameObject greenRings;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Use the ctrl keys to toggle the red rings' visibility
       /* if (Input.GetKeyDown("right ctrl") || Input.GetKeyDown("left ctrl"))
        {
            redRings.toggleRedRings();
        }*/

        //Use the spacebar to toggle the green ring's visibility
        if (Input.GetKeyDown("space"))
        {
            greenRings.SetActive(!greenRings.activeSelf);
        }


    }
}
