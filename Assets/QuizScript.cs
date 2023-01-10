using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizScript : MonoBehaviour
{
    // Define variables to hold the questions and answers
    
    private string[] questions = { 
    "What is the capital of France?", 
    "What is the largest planet in our solar system?", 
    "What is the currency of Japan?" 
    };

    public string[,] answers = { 
    {"Paris", "Marseille", "Lyon"}, 
    {"Earth", "Mars", "Jupiter"}, 
    {"Yen", "Dollar", "Euro"} 
    };

    // Define variables to hold references to UI elements
    public Text questionText;
    public Text scoreText;
    public Button[] answerButtons;

    // Other variables
    private int currentQuestionIndex;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        // Display the first question
        DisplayQuestion();
    }

    // Display the current question
    void DisplayQuestion()
    {
        // Get the current question and answers
        string question = questions[currentQuestionIndex];
        string[] options = GetAnswersForQuestion(currentQuestionIndex);

        // Display the question
        questionText.text = question;

        // Display the answers
        for (int i = 0; i < options.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = options[i];
        }
    }

    // Get the answers for the given question index
    string[] GetAnswersForQuestion(int questionIndex)
    {
        string[] options = new string[answers.GetLength(1)];
        for (int i = 0; i < answers.GetLength(1); i++)
        {
            options[i] = answers[questionIndex, i];
        }
        return options;
    }

    // Check the given answer for the current question
    public void CheckAnswer(int buttonIndex)
    {
        // Get the correct answer for the current question
        string correctAnswer = answers[currentQuestionIndex, 0];

        // Get the text of the button that was clicked
        string selectedAnswer = answerButtons[buttonIndex].GetComponentInChildren<Text>().text;

        // Check if the selected answer is correct
        if (selectedAnswer == correctAnswer)
        {
            // Increment the score
            score++;
        }

        // Go to the next question
        currentQuestionIndex++;

        // Check if all questions have been answered
        if (currentQuestionIndex >= questions.Length)
        {
            // End the quiz
            EndQuiz();
        }
        else
        {
            // Display the next question
            DisplayQuestion();
        }
    }

    // End the quiz and display the final score
    void EndQuiz()
    {
        // Display the final score
        scoreText.text = "Score: " + score + "/" + questions.Length;

        // Hide the question and answer buttons
        questionText.gameObject.SetActive(false);
        foreach (Button button in answerButtons)
        {
            button.gameObject.SetActive(false);
        }
    }
}
