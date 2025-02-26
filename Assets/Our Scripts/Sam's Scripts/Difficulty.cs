using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    public static string difficulty;

    private void Start()
    {
        // Default to random difficulty
        SetRandomDifficulty();
    }

    public static void SetMinorDifficulty()
    {
        difficulty = "Minor";
        Debug.Log("Difficulty set to Minor");
    }

    public static void SetSevereDifficulty()
    {
        difficulty = "Severe";
        Debug.Log("Difficulty set to Severe");
    }

    public static void SetRandomDifficulty()
    {
        int difficultyNum = Random.Range(0, 2);
        difficulty = (difficultyNum == 0) ? "Minor" : "Severe";
        Debug.Log("Difficulty set to " + difficulty);
    }
}