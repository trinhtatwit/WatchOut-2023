using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
* A pop up system that is responsible for opening and closing
* pop up windows
*/
public class PopUpSystem : MonoBehaviour
{
    public GameObject popUpBox;
    public Animator animator;
    public TMP_Text popUpText;
    public NpcMovement npcMovement;
    public PlayerMovement playerMovement;
    public GameObject playButton1;
    public GameObject playButton2;
    public GameObject closeButton1;
    public GameObject closeButton2;

    //For popping up boxes with Set text
    public void PopUp()
    {
        popUpBox.SetActive(true);
        //animator.SetTrigger("pop");
        npcMovement.stopMoving();
        playerMovement.stopMoving();
    }

    //For popping up boxes with changing text
    public void PopUp(string text)
    {
        popUpBox.SetActive(true);
        popUpText.text = text;
        animator.SetTrigger("pop");
        npcMovement.stopMoving();
        playerMovement.stopMoving();
    }

    //For closing the pop-ups
    public void Close()
    {
        popUpBox.SetActive(false);
        animator.SetTrigger("close");
        npcMovement.startMoving();
        playerMovement.startMoving();
    }
}
