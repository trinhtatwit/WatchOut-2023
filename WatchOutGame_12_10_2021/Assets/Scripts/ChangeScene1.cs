using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Class used to go from scene to scene, transitioning between "levels"

public class ChangeScene1 : MonoBehaviour
{
    public string sceneName;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    public MarketInventory playerInventory;
    public bool initialize;

    //Method for changing the scene when clicking a button (Ex: going from the shop back to the pharmacy)
    public void changeSceneTo(string sceneName)
    {
        if (initialize) //basically if the scene change
        {
            playerInventory.clear();
        }
        playerStorage.initialValue = playerPosition;
        SceneManager.LoadScene(sceneName);
    }

    //Method for changing the scene when the player walks into an object (Ex: walking into a door to leave a building)
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Checks if object colliding is the player based on the object's tag
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            changeSceneTo(sceneName);

        }
    }

}
