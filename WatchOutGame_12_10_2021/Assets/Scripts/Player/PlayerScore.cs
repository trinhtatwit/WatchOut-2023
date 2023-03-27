using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    // number of people the player contacts
    public int numOfContacts;
    // timer from start of the scene
    public float timeElapsed;
    // player score
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        numOfContacts = 0;
        timeElapsed = 0;
        score = 100;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
    }

    // called when the player comes steps into the red rings
    public void cameInContact()
    {
        score -= 5;
        numOfContacts++;
    }

    public float getTimeElapsed() 
    {
        return timeElapsed;
    }

     public int getScore() 
    {
        return score;
    }

     public int getNumOfContacts() 
    {
        return numOfContacts;
    }
}
