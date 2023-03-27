using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBehaviorScript : MonoBehaviour
{
    public GameObject exit;
    public GameObject player;
    public AudioSource popUpAudio;
    public static bool canLeave;

    void start()
    {
        Debug.Log("Lock = " + canLeave);
        canLeave = false;
    }

    //When the player tries to leave the level it opens a popup that informs them to speak to their parents, it displays the correct audio button and close button
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(PlayerStats.getMoney() >= 15){ //this is a really roundabout way to figure out if quests 0 and 1 are completed
                Debug.Log("unlocked door");
                canLeave = true;
            }
            if(!canLeave) {
                PopUpSystem pop = exit.GetComponent<PopUpSystem>();
                pop.playButton1.SetActive(false);
                pop.playButton2.SetActive(true);
                pop.closeButton1.SetActive(false);
                pop.closeButton2.SetActive(true);
                pop.PopUp("You should talk to both your parents before you leave.\nMake sure you're all prepared before you head off!");

                //after collision, you could still technically move forward and leave the scene, this is to make sure that this doesnt happen
                float y = player.transform.position.y;
                player.transform.position = new Vector3(player.transform.position.x, y + (float) 0.3, player.transform.position.z);
            }
        }
    }

    //Plays the connected audio
    public void playAudio()
    {
        popUpAudio.Play();
    }

    //closes the popup
    public void closePopUp()
    {
        PopUpSystem pop = exit.GetComponent<PopUpSystem>();
        popUpAudio.Stop();
        pop.Close();
    }
}
