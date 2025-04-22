using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class returntolobby : MonoBehaviour
{
    public void BackToTheLobby()
    {
        Debug.Log("Returning to Lobby VR...");
        SceneManager.LoadScene("Lobby VR");
    }
}
