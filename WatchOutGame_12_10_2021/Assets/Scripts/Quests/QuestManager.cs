using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;


/*
* Keeping track of all of the quests in the game
*/
public class QuestManager : MonoBehaviour
{
    public Quest quest;
    public bool completedQuest;
    public DialogueManager dialogueManager;
    public List<Quest> nextQuests;

    // Start is called before the first frame update
    void Start()
    {
        completedQuest = false;
    }

    public void startQuest() {
        dialogueManager.setDialogue(quest.getStartText());
//        dialogueManager.setAudio(quest.getStartAudio());
        completedQuest = false;
    }

    public void endQuest() {
        dialogueManager.setDialogue(quest.getEndText());
//        dialogueManager.setAudio(quest.getEndAudio());

        FindObjectOfType<PlayerMoneyScript>().gainMoney(quest.getReward());
        ReportQuestCompletion(quest.getQuestNum());


        if(nextQuests.Count > 0)
        {
            quest = nextQuests[0];
            nextQuests.RemoveAt(0);
        }else {
            completedQuest = true;
        }
    }

    public Quest GetQuest(){
        return quest;
    }

    public bool IsActive(){
        return !completedQuest;
    }

    //creating an in script custom event. However it can  be noted that it is also possible to do this in the inspector which may be easier
    public void ReportQuestCompletion(int questNum){
        AnalyticsEvent.Custom("Quest_Finished", new Dictionary<string, object>
        {
            { "quest_id", questNum },
        });
    }
}
