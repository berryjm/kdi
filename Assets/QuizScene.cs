using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizScene : MonoBehaviour
{
    public CheckboxData checkboxData;

    private void Start()
    {
        LoadLessons();
    }

    private void LoadLessons()
    {
        // Loop through the checkboxes in checkboxData
        for (int i = 0; i < checkboxData.checkboxStates.Length; i++)
        {
            Debug.Log("checkboxData: " + checkboxData.checkboxStates[i]);
            if (checkboxData.checkboxStates[i])
            {
                // If the checkbox at index i is checked, load the corresponding lesson files
            // Load Kanji file
            
            TextAsset kanjiTextAsset = Resources.Load<TextAsset>("Quiz/" + (i + 1) + "/kanji");
            string[] kanjiLines = kanjiTextAsset.text.Split("\n"[0]);
            // Do something with the kanji lines

            // Load English file
            TextAsset englishTextAsset = Resources.Load<TextAsset>("Quiz/" + (i + 1) + "/english");
            string[] englishLines = englishTextAsset.text.Split("\n"[0]);
            // Do something with the english lines

            // Load kunon file
            TextAsset kunonTextAsset = Resources.Load<TextAsset>("Quiz/" + (i + 1) + "/kunon");
            string[] kunonLines = kunonTextAsset.text.Split("\n"[0]);

            Debug.Log((i+1).ToString() + " added.");
            }
        }
    }
}
