using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDifficulty : MonoBehaviour
{
    public static string difficulty;

    public static void SetMinorDifficulty() //female
    {
        difficulty = "Minor"; 
        Debug.Log("Difficulty set to Minor!");
    }

    public static void SetMajorDifficulty()  //male
    {
        difficulty = "Major";
        Debug.Log("Difficulty set to Major!");
    }

    // public static void SetRandomDifficulty()
    // {
    //     int difficultyNum = Random.Range(0, 2);
    //     difficulty = (difficultyNum == 0) ? "Minor" : "Major";
    //     Debug.Log("Difficulty set to " + difficulty);
    // }
}