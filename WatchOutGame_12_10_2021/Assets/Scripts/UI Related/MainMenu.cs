using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Manages Main Menu
public class MainMenu : MonoBehaviour
{
    //Starts game
   public void PlayGame()
   {
       PlayerStats.resetStats();
       SceneManager.LoadScene(3);
       
   }

    //Exits application
   public void QuitGame()
   {
       Debug.Log("QUIT");
       Application.Quit();
   }
}
