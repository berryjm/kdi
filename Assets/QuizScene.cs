using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizScene : MonoBehaviour
{
    public CheckboxData checkboxData;
    public Button button1;
    public Button button2;
    public Button button3;
    public Text mainText;
    public Text debugText;
    //Arrays to store the lines from the text files
    private List<string> kanji = new List<string>();
    private List<string> english = new List<string>();
    private List<string> kunon = new List<string>();

    private int randomIndex;
    private string realAnswer;

    private void Start()
    {
        LoadLessons();
        button1.onClick.AddListener(() => CheckAnswer(button1.GetComponentInChildren<Text>().text));
        button2.onClick.AddListener(() => CheckAnswer(button2.GetComponentInChildren<Text>().text));
        button3.onClick.AddListener(() => CheckAnswer(button3.GetComponentInChildren<Text>().text));
        GetQuestion();
    }

    private void LoadLessons()
    {
        // Loop through the checkboxes in checkboxData
        for (int i = 0; i < checkboxData.checkboxStates.Length; i++)
        {
            // Debug.Log("checkboxData: " + checkboxData.checkboxStates[i]);
            if (checkboxData.checkboxStates[i])
            {
            // If the checkbox at index i is checked, load the corresponding lesson files
            // Load Kanji file
            
            TextAsset kanjiTextAsset = Resources.Load<TextAsset>("Quiz/" + (i + 1) + "/kanji");
            string[] kanjiLines = kanjiTextAsset.text.Split("\n"[0]);

            // Load English file
            TextAsset englishTextAsset = Resources.Load<TextAsset>("Quiz/" + (i + 1) + "/english");
            string[] englishLines = englishTextAsset.text.Split("\n"[0]);

            // Load kunon file
            TextAsset kunonTextAsset = Resources.Load<TextAsset>("Quiz/" + (i + 1) + "/kunon");
            string[] kunonLines = kunonTextAsset.text.Split("\n"[0]);

            // Debug.Log((i+1).ToString() + " added.");

            kanji.AddRange(kanjiLines);
            english.AddRange(englishLines);
            kunon.AddRange(kunonLines);
            }
        }
    }

    public void GetQuestion()
    {
        //Get a random line from the english pool
        randomIndex = Random.Range(0, english.Count);
        string englishWord = english[randomIndex];

        //Print the question
        // Debug.Log("What is the kanji for " + englishWord + "?");

        //Populate the buttons with the possible answers
        //Real answer
        realAnswer = kanji[randomIndex];
        //Fake answers
        string fakeAnswer1 = kanji[(randomIndex + 1) % kanji.Count];
        string fakeAnswer2 = kanji[(randomIndex + 2) % kanji.Count];

        //Create a list of the answers
        List<string> answers = new List<string>() {realAnswer, fakeAnswer1, fakeAnswer2};

        //Shuffle the list of answers
        for (int i = 0; i < answers.Count; i++)
        {
            int randomIndex2 = Random.Range(i, answers.Count);
            string temp = answers[i];
            answers[i] = answers[randomIndex2];
            answers[randomIndex2] = temp;
        }

        mainText.text = "What is the kanji for " + englishWord + "?";

        // Assign the answers to the buttons
        button1.GetComponentInChildren<Text>().text = answers[0];
        button2.GetComponentInChildren<Text>().text = answers[1];
        button3.GetComponentInChildren<Text>().text = answers[2];
    }

    public void CheckAnswer(string buttonText)
    {
        if (buttonText == realAnswer)
        {
            // Debug.Log("Correct!");
            debugText.text = "Correct!";
            GetQuestion();
        }
        else
        {
            // Debug.Log("Incorrect. The correct answer was " + realAnswer + ".");
            debugText.text = "Incorrect. The correct answer was " + realAnswer + ".";
            GetQuestion();
        }
    }
}
