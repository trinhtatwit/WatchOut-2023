using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//System for managing when player loses from hp loss & wins from completing task
public class WinLoseManagement : MonoBehaviour
{
    public Transform playerCamera;
    public GameObject NPCRing;
    public GameObject player;
    public Text  levelComplete, gameOver, gameOverHint;
    public PlayerScore playerScore;

    // Start is called before the first frame update
    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    //Triggered when player runs out of hp
    public void OnPlayerLose()
    {
        playerCamera.parent = null;
        Destroy(player);
        gameOver.enabled = true;
        gameOverHint.enabled = true;
        Invoke("ReloadScene", 3);
    }

    public void OnPlayerWin()
    {
    
        playerCamera.parent = null;
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<BoxCollider2D>().enabled = false;
        levelCompleteText();
        
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

     private void levelCompleteText()
    {
        string text = string.Format("Time spent outside: {0:0.00} \n", playerScore.getTimeElapsed());
        text += string.Format("You came in contact with {0} people \n", playerScore.getNumOfContacts());
        text += string.Format("Score: {0}", playerScore.getScore());
        levelComplete.text = text;
        levelComplete.enabled = true;
    }
    
}
