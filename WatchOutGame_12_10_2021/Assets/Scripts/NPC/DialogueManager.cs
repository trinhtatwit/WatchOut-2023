using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

/*
* Dialogue system that types out and displays text
*/
public class DialogueManager : MonoBehaviour
{ 
    public TMP_Text pressE; 
    public TextMeshProUGUI displayedText; // Where the text will actually be displayed
    public string[] dialogue; //Array of dialogue lines
//    public AudioSource[] dialogueAudio; //Array of voice lines for the current dialogue
    private int index; //Index of current dialogue line
    public float typingSpeed; //Speed at which the text is typed
    public GameObject dialogBox; //Box where dialogue is displayed
    public GameObject nextButton; //Button for progressing the dialogue
    public bool isBoxActive; //Bool for NPCs and player to stop them from moving if the box is active

    private PlayerMovement player; //To stop the players movement

    //At the start of the scene it gets the player thru their player movement script, sets the index to 0, and makes all necessary things false
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        index = 0;
        displayedText.text = "";
        isBoxActive = false;
        pressE.enabled = false;
        dialogBox.SetActive(false);
        nextButton.SetActive(false);
        
    }

    //Constantly checks if the displayed text matches the current dialogue line, if true sets the next button to true (This stops the player from moving too quickly through the text, breaks it otherwise)
    void Update()
    {
        if (displayedText.text == dialogue[index]) {
            nextButton.SetActive(true);
        }
    }

    //Starts the dialogue process, shows the box, makes the player stop moving, and starts the typing process
    public void displayDialog() {
        dialogBox.SetActive(true);
        player.stopMoving();
        isBoxActive = true;
        StartCoroutine(Type());    
    }

    //For typing out the dialogue
    IEnumerator Type()
    {
        foreach(char letter in dialogue[index].ToCharArray()) {
            displayedText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        
    }

    //Progresses the dialogue to the next sentence
    public void nextSentence()
    {
        nextButton.SetActive(false);

        if (index < dialogue.Length - 1)
        {
    //        dialogueAudio[index].Stop();
            index++;
            displayedText.text = "";
            StartCoroutine(Type());
        } 
        else {
            dialogBox.SetActive(false);
            nextButton.SetActive(false);
            isBoxActive = false;
            displayedText.text = "";
    //        dialogueAudio[index].Stop();
            index = 0;
            player.startMoving();
        }

    }

    //Sets the current dialogue based on the current quest
    public void setDialogue(string[] newDialog)
    {
        this.dialogue = newDialog;
    }

    //Sets the current audio based on the current quest
//    public void setAudio(AudioSource[] newAudio) 
 //   {
//       this.dialogueAudio = newAudio;
//    }

    //Plays the audio if the audio button is pressed
//    public void playAudio() 
//    {
//        dialogueAudio[index].Play();  
//    }

    //Methods for displaying the interact instruction on the NPCs 
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            pressE.enabled = true;
        }
    }
    //Method for when leaving trigger collider of NPC
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            pressE.enabled = false;
        }
    }

}