using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDifficulty : MonoBehaviour
{
public static string difficulty;

    public static void SetMinorDifficulty()
    {
        difficulty = "Minor";
        Debug.Log("Difficulty set to Minor");
    }

    public static void SetMajorDifficulty()
    {
        difficulty = "Major";
        Debug.Log("Difficulty set to Major");
    }

}