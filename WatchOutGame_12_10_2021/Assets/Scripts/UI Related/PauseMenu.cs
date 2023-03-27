using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Manages pause menu while playing game
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject uiMenu;
    public static bool GameIsPaused = false;
    

    // Update is called once per frame
    void Update()
    {
        //Pause when player presses ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        //Leaves pause menu and goes back to game
        pauseMenuUI.SetActive(false);
        uiMenu.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        //Pauses game and opens pause menu
        pauseMenuUI.SetActive(true);
        uiMenu.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        //Goes to main menu
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        //Exits game
        Application.Quit();
    }

    //thing to implement, an options menu
    //If there is an options menu, make sure the player can access it through clicking the options button in the PauseMenu located in Canvas
}
