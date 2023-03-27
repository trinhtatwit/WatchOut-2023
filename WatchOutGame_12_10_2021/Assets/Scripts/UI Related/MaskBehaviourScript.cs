using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Mask game object behavior script
*/
public class MaskBehaviourScript : MonoBehaviour
{
    public GameObject mask;
    public Inventory playerInventory;
    public InventoryItem maskItem;
    public InventoryManager inventoryManager;
    public AudioSource popUpAudio;

    
    //When the player walks over the mask in their room it displays the popup with the appropriate buttons, the buttons that read the correct text, and the correct closing button
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            playerInventory.addMask(maskItem);
            inventoryManager.MakeInventorySlots();
            mask.SetActive(false);
            PopUpSystem pop = mask.GetComponent<PopUpSystem>();
            pop.playButton1.SetActive(true);
            pop.playButton2.SetActive(false);
            pop.closeButton1.SetActive(true);
            pop.closeButton2.SetActive(false);
            pop.PopUp("You picked up a mask. \nPress 'I' to use this protective equipment to reduce your chances of catching covid-19.\nStay safe out there!");
        
        }
    }

    //Plays the audio connected to the object's popup 
    public void playAudio() 
    {
        popUpAudio.Play();
    }

    //Used to close the popup
    public  void closePopUp()
    {
        PopUpSystem pop = mask.GetComponent<PopUpSystem>();
        popUpAudio.Stop();
        pop.Close();
    }
}
