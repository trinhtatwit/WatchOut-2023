using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Attempt to change level while storing player position
public class LevelManager : MonoBehaviour
{
   public Transform player;
   private Dictionary<int, Vector3> startingPosition = new Dictionary<int, Vector3>();
    
    void Start(){
        DontDestroyOnLoad(gameObject);
    }

    public void LoadNewLevel(int level) {
        startingPosition[SceneManager.GetActiveScene().buildIndex] = player.position;
        SceneManager.LoadScene(level);
    }
 
    public void OnLevelWasLoaded(int level) {
        if (startingPosition.ContainsKey(level)) player.position = startingPosition[level];
    }
}

