using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField]
    private int startingScore = 50;

    [SerializeField]
    private int maxScore = 100;

    private int currentScore;
    private float startTime;
    private bool timerStarted = false;

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

    public void PrintScore()
    {
        int score = GetTotalScore();
        string grade = GetLetterGrade();
        Debug.Log($"Final Score: {score} ({grade})");
        currentScore = startingScore;
    }
}