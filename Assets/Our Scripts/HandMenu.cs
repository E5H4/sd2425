using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class HandMenu : MonoBehaviour
{
    public GameObject wristUI;

    public bool activeWristUI = true;

    // Start is called before the first frame update
    void Start()
    {
        DisplayWristUI();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayWristUI()
    {
        // When the active wrist is active
        if (activeWristUI)
        {
            wristUI.SetActive(false);
            activeWristUI = false;

        }else if (!activeWristUI)
        {
            wristUI.SetActive(true);
            activeWristUI = true;
        }
    }

    public void PauseBottonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
            DisplayWristUI();
    }

    public void GotoLobby()
    {
        SceneManager.UnloadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(0);
        //SceneTransitionManager.singleton.GoToSceneAsync(0);

    }
}
