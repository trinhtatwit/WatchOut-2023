using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Old script for ending level
//Could be used to end game upon completion in current version
public class EndOfLevel : MonoBehaviour
{
    public WinLoseManagement winLoseScript;

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.tag == "Player")
        {
        winLoseScript.OnPlayerWin();
        }
    }
}