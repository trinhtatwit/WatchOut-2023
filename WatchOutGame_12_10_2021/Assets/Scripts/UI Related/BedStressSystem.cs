using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Systems used for reducing the player's tiredness meter

public class BedStressSystem : MonoBehaviour
{
    public GameObject bed;
    public GameObject sleepScreen;
    public GameObject sleepText;
    public TextMeshProUGUI sleepText2;
    public GameObject[] text;
    public GameObject mainMenu;
    public PlayerMovement player;
    public PlayerStressScript playerStress;
    public int x;
    public WinLoseManagement winLose;

    //At the start of the scene it sets the iterator to 0, sets the sleep screen and all its parts off
    void Start() 
    {
        x = 0;
        sleepScreen.SetActive(false);

        for (int i = 0; i < text.Length; i++)
        {
            text[i].SetActive(false);
        }
        
        sleepText.SetActive(false);
    }

    //Once the player enters the bed it checks their stress level, if its under 100 it tells them they can come back later to reduce it, if its above 100
    //it activates the sleep screen, "plays" a small animation to indicate sleeping and resets their tiredness
    //Once the player has gotten the vaccine, this instead displays the win stats and thanks them for playing the game
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerStats.getStress() < 100)
            {
                PopUpSystem pop = bed.GetComponent<PopUpSystem>();
                pop.PopUp("If you have too many sad faces come back here and rest up!\nIt'll make you feel much better!");
            }
            else 
            {
                if (!PlayerStats.vaccineGot)
                {
                    //Opens sleep screen for a few seconds
                    sleepScreen.SetActive(true);
                    playerStress.loseStress(100);
                    player.stopMoving();

                    StartCoroutine(waitForNext());
                    player.startMoving();
                }
                else 
                {
                    sleepScreen.SetActive(true);
                    player.stopMoving();
                    sleepText.SetActive(true);
                    sleepText2.text = "Congrats! You beat WatchOut! We hope you learned a thing or two about\nCovid-19 and are more prepared to protect yourself against it!";
                    mainMenu.SetActive(true);
                    winLose.OnPlayerWin();
                }
                


            }
        }
    }

    //Used to add wait spaces between each Z in the sleep animation
    IEnumerator waitForNext()
    {
        text[0].SetActive(true);
        yield return new WaitForSeconds(1);
        text[1].SetActive(true);
        yield return new WaitForSeconds(1);
        text[2].SetActive(true);
        yield return new WaitForSeconds(1);
        sleepText.SetActive(true);
        yield return new WaitForSeconds((float) 1.5);
        sleepScreen.SetActive(false);
    }

    //Closes the bed indicator popup
    public void closePopUp()
    {
        PopUpSystem pop = bed.GetComponent<PopUpSystem>();
        pop.Close();
    }
}
