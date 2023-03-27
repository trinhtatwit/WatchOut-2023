using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class used in the pharmacy to trigger the vaccination event where the player "gets" the vaccine and can end the game

public class VaccineTrigger : MonoBehaviour
{
    public GameObject vaccination;

    //Makes sure the vaccination object is not active when the scene is loaded
    void Start()
    {
        vaccination.SetActive(false);
    }

    //Constantly checks if the player bought the vaccine, once they get the vaccine it does not happen again so the player can't repeatedly get vaccinated
    void Update()
    {
        if (PlayerStats.vaccineBought && !PlayerStats.vaccineGot)
        {
            vaccination.SetActive(true);
        }
    }
}
