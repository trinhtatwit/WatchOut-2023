using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Attempt to store player position
public class PlayerPosition: MonoBehaviour
{
    public float x,y,z;
    
    public void Save()
    {
    x = transform.position.x;
    y = transform.position.y;
    z = transform.position.z;

    PlayerPrefs.SetInt ("currentscenesave",SceneManager.GetActiveScene().buildIndex);
    PlayerPrefs.SetFloat ("x",x);
    PlayerPrefs.SetFloat ("y",y);
    PlayerPrefs.SetFloat ("z",z);
       
    }

    public void Load()
    { 
    //loadScene ();
    
    x = PlayerPrefs.GetFloat ("x");
    y = PlayerPrefs.GetFloat ("y");
    z = PlayerPrefs.GetFloat ("z");
    transform.position = new Vector3 (x, y, z);
    }
    
    public int getScene()
    {
    return PlayerPrefs.GetInt ("currentscenesave");
    }

    public void setPosition(float x2, float y2, float z2) 
    {
    transform.position = new Vector3 (x2, y2, z2);
    }
}
