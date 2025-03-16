using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ValidateDifficulty : MonoBehaviour
{
    void Start()
    {
        string difficulty = PlayerPrefs.GetString("Difficulty", "None"); // Get stored difficulty

        if (difficulty != "Minor") // Check if it's incorrect
        {
            Debug.LogWarning("Access denied! Redirecting to the main menu.");
            SceneManager.LoadScene("MainMenu"); // Redirect to main menu or appropriate scene
        }
        else
        {
            Debug.Log("Access granted to MinorDifficulty scene.");
        }
    }
}