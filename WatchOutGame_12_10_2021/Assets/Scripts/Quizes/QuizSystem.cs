using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Analytics;


//Part of quiz system for school activity modelled after quest system
public class QuizSystem : MonoBehaviour
{
    //Quiz Objects
    public GameObject[] options; //Array of buttons on the scene
    public GameObject startButton;
    public TextMeshProUGUI question; //Question Label

    //Quiz "Data"
    public int currentQuestion;
    public int numberOfCorrectAnswers;
    public Quiz currentQuiz;
    public List<Quiz> quizzes = new List<Quiz>(); //List of possible Quizzes
    public List<QuizQuestion> quizQuestions = new List<QuizQuestion>(); //List of questions in the current quiz
    public List<string> currentQuestionAnswers = new List<string>(); //List of the answers to each question
    public List<QuizQuestion> AnsweredQuestions = new List<QuizQuestion>(); //List of questions that have been answered, created to be put back into the quiz once the player leaves the quiz screen

    //Player Stats
    public PlayerMoneyScript playerMoney;
    public PlayerStressScript playerStress;

    // Start is called before the first frame update
    void Start()
    {
        numberOfCorrectAnswers = 0;
        if (playerStress.stress >= 100) 
        {
            question.text = "You're too tired right now, go rest up and come back later!";
            startButton.SetActive(false);

            for (int i = 0; i < 4; i++)
            {
                options[i].SetActive(false);
            }
        }
        else
        {
            currentQuiz = quizzes[Random.Range(0, quizzes.Count)];
            quizQuestions = currentQuiz.getQuestions();
            startButton.SetActive(true);

            for (int i = 0; i < 4; i++)
            {
                options[i].SetActive(false);
            }

            question.text = "Welcome to the Covid Quiz! Are you ready to begin?";
        }
    }

    //Begins the quiz,
    public void startQuiz() 
    {
        startButton.SetActive(false);
        generateQuestion();
        
    }

    //Gets a random question in the current list of quiz questions, removes it from the list, and gets the list of answers for that question, then sets the label to that question
    public void generateQuestion()
    {   
        if (quizQuestions.Count > 0)
        {
            currentQuestion = Random.Range(0, quizQuestions.Count);
            question.text = quizQuestions[currentQuestion].getQuestion();

            for (int j = 0; j < 4; j++)
            {
                currentQuestionAnswers.Add(quizQuestions[currentQuestion].getAnswers()[j]);
            }
            
            setAnswers();
        }
        else 
        {
            clearOptions();
            question.text = "You've answered all the questions! Good Job!";
            QuizCompletion(currentQuiz.getQuizNumber(),quizQuestions.Count,numberOfCorrectAnswers);
        }
    }

    //Chooses a random answer for the current question and set the button to that answer and removes it from the answer list
    public void setAnswers() 
    {
        showOptions();
        for (int i = 0; i < options.Length; i++) 
        {
            int x = Random.Range(0, 4-i);
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (char) (65+i) + ". " + currentQuestionAnswers[x];
            currentQuestionAnswers.RemoveAt(x);
        }
    }

    //Checks if button pressed was the correct answer to the question and calls the appropriate method
    public void pressedA() 
    {
        clearOptions();
       
        if (options[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains(quizQuestions[currentQuestion].getAnswers()[3]))
        {
            correct();
        }
        else 
        {
            incorrect();
        }
    }

    public void pressedB()
    {
        clearOptions();
       
        if (options[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains(quizQuestions[currentQuestion].getAnswers()[3]))
        {
            correct();
        }
        else
        {
            incorrect();
        }
    }

    public void pressedC()
    {
        clearOptions();
       
        if (options[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains(quizQuestions[currentQuestion].getAnswers()[3]))
        {
            correct();
        }
        else
        {
            incorrect();
        }
    }

    public void pressedD()
    {
        clearOptions();
       
        if (options[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Contains(quizQuestions[currentQuestion].getAnswers()[3]))
        {
            correct();
        }
        else
        {
            incorrect();
        }
    }

    //If correct, displays a correct phrase and gives the player $5 and increases stress by 5
    public void correct() 
    {
        numberOfCorrectAnswers++;
        QuizAnswerResults(currentQuiz.getQuizNumber(),currentQuestion,true);
        question.text = "Correct! You win $5!";
        playerMoney.gainMoney(5);
        playerStress.gainStress(10);
        AnsweredQuestions.Add(quizQuestions[currentQuestion]);
        quizQuestions.RemoveAt(currentQuestion);
        StartCoroutine(waitForNext());
    }
    
    //If incorrect, displays a incorrect phrase and increases stress by 20
    public void incorrect() 
    {
        QuizAnswerResults(currentQuiz.getQuizNumber(),currentQuestion,false);
        question.text = "Incorrect :(";
        playerStress.gainStress(20);
        AnsweredQuestions.Add(quizQuestions[currentQuestion]);
        quizQuestions.RemoveAt(currentQuestion);
        StartCoroutine(waitForNext());
    }

    IEnumerator waitForNext() 
    {
        yield return new WaitForSeconds(1);
        generateQuestion();
    }

    void clearOptions() 
    {
        for (int i = 0; i < 4; i++)
        {
            options[i].SetActive(false);
        }
    }

    void showOptions() 
    {
        for (int i = 0; i < 4; i++)
        {
            options[i].SetActive(true);
        }
    }

    //Resets the removed quiz questions once the player leaves the quiz screen
    public void FixQuiz() 
    {
        while(AnsweredQuestions.Count > 0) 
        {
            quizQuestions.Add(AnsweredQuestions[0]);
            AnsweredQuestions.RemoveAt(0);
        }
    }

    public void QuizAnswerResults(int quizNum, int questionNum, bool gotCorrect)
    {
        AnalyticsEvent.Custom("Question_Answered", new Dictionary<string, object>
        {
            { "quiz_id", quizNum },
            { "question_number", questionNum },
            { "got_correct", gotCorrect}
        });
    }

    public void QuizCompletion(int quizNum, int total_questions, int correct_answers)
    {
        AnalyticsEvent.Custom("Quiz_Completed", new Dictionary<string, object>
        {
            { "quiz_id", quizNum },
            { "number_of_questions", total_questions },
            { "correct_answers", correct_answers}
        });
    }

}
