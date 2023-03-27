using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New quiz", menuName = "Quiz/Covid Quiz")]
//Part of quiz system for school activity modelled after quest system
public class Quiz : ScriptableObject
{
    public int quizNum;
    public List<QuizQuestion> questions = new List<QuizQuestion>();

    //Get methods, just in case
    public int getQuizNumber() 
    { 
        return quizNum; 
    }
    
    public List<QuizQuestion> getQuestions() 
    { 
        return questions; 
    }
}
