using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Quest game object that holds all the information regarding quests
*/
public class Quest : MonoBehaviour
{
    public int questNum;
    public string questName;
    public string[] startQuestText;
    // public AudioSource[] startQuestAudio;
    public string[] endQuestText;
    // public AudioSource[] endQuestAudio;
    public float questReward;
    //public QuestManager questManager;

    public Quest(string name, string[] startText, string[] endText, float reward)
    {
         this.questName = name;
         this.startQuestText = startText;
         this.endQuestText = endText;
         this.questReward = reward;
    }
    
    public int getQuestNum()
    {
        return questNum;
    }

    public string getName()
    { 
        return questName; 
    }

    public string[] getStartText()
    { 
        return startQuestText; 
    }

    public string[] getEndText()
    { 
        return endQuestText; 
    }

/*     public AudioSource[] getStartAudio() 
    { 
        return startQuestAudio; 
    }

    public AudioSource[] getEndAudio() 
    { 
        return endQuestAudio; 
    }
 */
    public float getReward()
    { 
        return questReward; 
    }
        

}
