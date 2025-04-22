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