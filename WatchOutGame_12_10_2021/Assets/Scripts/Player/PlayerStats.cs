using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//System to keep player stats between levels and keep track of flags
public static class PlayerStats
{
    //Stats
    public static float health = 50f;
    public static float money = 10;
    public static float points = 0;
    public static float stress = 0;
    
    //Flags for finishing game
    public static bool vaccineBought = false;
    public static bool vaccineSet = false;
    public static bool vaccineGot = false;

    //Sets to default value for when player leaves to main menu but not exiting application
    public static void resetStats()
    {
        health = 50f;
        money = 0;
        points = 0;
        stress = 0;
        vaccineBought = false;
        vaccineSet = false;
        vaccineGot = false;
    }

    //"get..." methods returns current global stat for other scripts to use
    //"set..." methods updates global stat to new value from other scripts
    public static float getHealth() 
    {
        return health;
    }

    public static void setHealth(float a)
    {
        health = a;
    }

    public static float getMoney() 
    {
        return money;
    }

    public static void setMoney(float b)
    {
        money = b;
    }


    public static float getPoints() 
    {
        return points;
    }
    public static void setPoints(float c)
    {
        points = c;
    }


    public static float getStress() 
    {
        return stress;
    }


    public static void setStress(float d)
    {
        stress = d;
    }

    //Sets flags to true
    public static void boughtVaccine()
    {
        vaccineBought = true;
    }


    public static void setVaccine()
    {
        vaccineSet = true;
    }


    public static void gotVaccine()
    {
        vaccineGot = true;
    }
}
