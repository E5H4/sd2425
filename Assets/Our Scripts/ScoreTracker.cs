using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystemWithText;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField]
    private int startingScore = 50;

    [SerializeField]
    private int maxScore = 100;

    private int currentScore;
    private float startTime;
    private bool timerStarted = false;

    [SerializeField] private GameObject[] gradePanels;  // A, B, C, D, F in order
    [SerializeField] private DialogueUIController[] gradeDialogues; 
    [SerializeField] private TextMeshProUGUI[] scoreTexts;


    //Print out on screen
    [SerializeField] private TextMeshProUGUI scoreText;

    void Start()
    {
        currentScore = startingScore;
        Debug.Log("Starting Score: " + currentScore);
    }


    // --- Add points ---
    public void AddOnePoint()
    {
        ModifyScore(1);
    }

    public void AddThreePoints()
    {
        ModifyScore(3);
    }

    public void AddFivePoints()
    {
        ModifyScore(5);
    }

    // --- Subtract points ---
    public void SubtractOnePoint()
    {
        ModifyScore(-1);
    }

    public void SubtractThreePoints()
    {
        ModifyScore(-3);
    }

    public void SubtractFivePoints()
    {
        ModifyScore(-5);
    }

    // --- General score update ---
    private void ModifyScore(int amount)
    {
        currentScore += amount;
        currentScore = Mathf.Clamp(currentScore, 0, maxScore); // keep score within 0 and 100
        Debug.Log("Score Updated: " + currentScore);
        UpdateScoreDisplay();
    }
 

    public void StartTimer()
    {
        startTime = Time.time;
        timerStarted = true;
        Debug.Log("Timer Started.");
    }

    public void EndTimer()
    {
        if (!timerStarted)
        {
            Debug.LogWarning("Timer was not started.");
            return;
        }

        float endTime = Time.time;
        float elapsedTime = (endTime - startTime) / 60f; // convert to minutes

        Debug.Log($"Game completed in {elapsedTime:F2} minutes.");


        if (elapsedTime <= 5f)
        {
            AddFivePoints();
        }
        else if (elapsedTime <= 8f)
        {
            AddThreePoints();
        }
        else if (elapsedTime <= 10f)
        {
            AddOnePoint();
        }
        else if (elapsedTime >= 20f)
        {
            SubtractFivePoints();
        }
        else if (elapsedTime >= 15f)
        {
            SubtractThreePoints();
        }
        else if (elapsedTime >= 11f)
        {
            SubtractOnePoint();
        }

        timerStarted = false;
    }

    //Print percent on screen
    private void UpdateScoreDisplay()
    {
        if (scoreTexts != null && scoreTexts.Length > 0)
        {
            foreach (TextMeshProUGUI text in scoreTexts)
            {
                if (text != null)
                {
                    text.text = currentScore.ToString() + "%";
                }
            }
        }
    }


    // --- Get the total score ---
    public int GetTotalScore()
    {
        return currentScore;
    }

    public string GetLetterGrade()
    {
        switch (currentScore)
        {
            case int n when n >= 90:
                return "A";
            case int n when n >= 80:
                return "B";
            case int n when n >= 70:
                return "C";
            case int n when n >= 60:
                return "D";
            default:
                return "F";
        }
    }

    //Print score in console, NOT ON SCREEN
    public void PrintScore()
    {
        int score = GetTotalScore();
        string grade = GetLetterGrade();
        Debug.Log($"Final Score: {score} ({grade})");
        //currentScore = startingScore;
    }


    //MAKE SURE TO RESET SCORE BEFORE ENDING YOUR GAME
    public void ResetScore()
    {
        currentScore = startingScore;
    }


    //FOR SAM'S SCENE UNLESS YALL WANNA DO IT TOO (hi sam -E)
    //Arrays
    private int GradeToIndex(string grade)
    {
        switch (grade)
        {
            case "A": return 0;
            case "B": return 1;
            case "C": return 2;
            case "D": return 3;
            default: return 4; // F or anything else goes to index 4
        }
    }

    //Pop-ups
    public void SamsPrintScore()
    {
        Debug.Log("Im calling the grades");
        string grade = GetLetterGrade();
        int gradeIndex = GradeToIndex(grade);

        // Disable all grade panels first
        for (int i = 0; i < gradePanels.Length; i++)
        {
            gradePanels[i].SetActive(false);
        }

        if (gradeIndex >= 0 && gradeIndex < gradePanels.Length)
        {
            // Enable the correct one
            gradePanels[gradeIndex].SetActive(true);
            Debug.Log("Pannel Enabled");

            // Show the correct dialogue
            if (gradeDialogues[gradeIndex] != null)
            {
                gradeDialogues[gradeIndex].ShowDialogueUI();
                Debug.Log("Dialogue called");
            }
            UpdateScoreDisplay();
        }
        else
        {
            Debug.LogWarning("Grade not recognized: " + grade);
        }
    }

}