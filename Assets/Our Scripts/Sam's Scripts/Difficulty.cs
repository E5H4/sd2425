using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    public static string difficulty;
    [Header("Character")]
    [SerializeField] private GameObject CharReg;
    [SerializeField] private GameObject CharSev;

    public void SetMinorDifficulty()
    {
        difficulty = "Minor";
        Debug.Log("Difficulty set to Minor");
    }

    public void SetSevereDifficulty()
    {
        difficulty = "Severe";
        Debug.Log("Difficulty set to Severe");
        SwitchChar();

    }

    public void SetRandomDifficulty()
    {
        int difficultyNum = Random.Range(0, 2);
        difficulty = (difficultyNum == 0) ? "Minor" : "Severe";
        Debug.Log("Difficulty set to " + difficulty);
    }

    public void SwitchChar()
    {
        CharReg.SetActive(false);
        CharSev.SetActive(true);
    }
}