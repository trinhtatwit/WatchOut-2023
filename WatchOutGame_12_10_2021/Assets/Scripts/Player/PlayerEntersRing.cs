using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEntersRing : MonoBehaviour
{
    public GameObject SixFootWarning;
    public static bool takingDamage;
    public PlayerScore playerScore;

    void Start()
    {
       SixFootWarning.SetActive(false);
       takingDamage = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("right ctrl") || Input.GetKeyDown("left ctrl"))
        {
            toggleRedRings();
        }
    }

    //player takes damage upon entering ring
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "player")
        {
            //print("player entered 6foot ring");
            SixFootWarning.SetActive(true);
            takingDamage = true;
            playerScore.cameInContact();
        }
    }

    //player stops taking damage when leaving ring
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "player")
        {
            //print("player left 6foot ring");
            SixFootWarning.SetActive(false);
            takingDamage = false;
        }
    }
    
    //Toggles red ring visibility (called in MiscellaneousControls.cs)
    public void toggleRedRings()
    {
        GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
    }
}
