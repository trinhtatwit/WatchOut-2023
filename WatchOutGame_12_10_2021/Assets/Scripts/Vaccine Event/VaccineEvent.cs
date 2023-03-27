using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Creates the finale event, once the player buys the vaccine the mom npc shows up so that the players know that they need their parents present to be vaccinated
//In addition, the mom directs the player to speak to the correct npc and then reminds them to go home once vaccinated

public class VaccineEvent : MonoBehaviour
{
    public DialogueManager dialog; //Pharmacy Clerk's dialogue
    public PlayerStressScript playerStress; //For changing the player's stress to allow them to sleep in the bed
    public GameObject mom; 
    public GameObject vaccineEvent;

    //At the start of the scene, makes the mom appear if the player has bought the vaccine, and sets her first set of dialogue
    void Start() 
    {
        if (PlayerStats.vaccineBought) 
        {
            mom.SetActive(true);
            mom.GetComponent<QuestManager>().startQuest();
        }  
    }

    //Checks if the player has gotten the vaccine, once they do it sets the second set of dialogue, and makes the vaccine event go away so they can't trigger it again
    void Update() 
    {
        if (PlayerStats.vaccineGot) 
        {
            mom.GetComponent<QuestManager>().endQuest();
            vaccineEvent.SetActive(false);
        }


    }
    
    //When the player walked in front of the clerk it triggers the dialogue that "gives" them the vaccine
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialog.displayDialog();
        }
        
    }

    //Once the player leaves the space it increases their stress so they can sleep in the bed and also sets it so they took the vaccine
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerStress.gainStress(100);
            takeVaccine();
        }
    }

    public void takeVaccine()
    {
        PlayerStats.gotVaccine();
    }
}
