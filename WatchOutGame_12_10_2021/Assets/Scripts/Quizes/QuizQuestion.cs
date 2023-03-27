using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Quiz/Quiz Question")]
[System.Serializable]
public class QuizQuestion : ScriptableObject
{
    public string Question;
    public string[] Answers;

    //Get methods, just in case
    public string getQuestion() { return Question; }
    public string[] getAnswers() { return Answers; }
}
