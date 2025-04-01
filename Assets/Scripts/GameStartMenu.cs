using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartMenu : MonoBehaviour
{
    [Header("UI Pages")]
    public GameObject mainMenu;
    public GameObject options;
    public GameObject about;
    public GameObject Scenarios;

    [Header("Main Menu Buttons")]
    //public Button startButton;
    public Button optionButton;
    public Button aboutButton;
    public Button quitButton;
    public Button scenariosButton;

    [Header("Scenario Buttons")]
    public Button blsButton;
    public Button bulletButton;
    public Button heartButton;
    public Button hypoglycemicButton;


    public List<Button> returnButtons;

    // Start is called before the first frame update
    void Start()
    {
        EnableMainMenu();

        //Hook events
        //startButton.onClick.AddListener(StartGame);
        optionButton.onClick.AddListener(EnableOption);
        aboutButton.onClick.AddListener(EnableAbout);
        quitButton.onClick.AddListener(QuitGame);
        scenariosButton.onClick.AddListener(EnableScenarios);

        blsButton.onClick.AddListener(LoadBLSScene);
        bulletButton.onClick.AddListener(LoadBulletScene);
        heartButton.onClick.AddListener(LoadHeartScene);
        hypoglycemicButton.onClick.AddListener(LoadHypoglycemicScene);


        foreach (var item in returnButtons)
        {
            item.onClick.AddListener(EnableMainMenu);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(1);
    }

    public void HideAll()
    {
        mainMenu.SetActive(false);
        options.SetActive(false);
        about.SetActive(false);
        Scenarios.SetActive(false); // Added
    }

    public void EnableMainMenu()
    {
        mainMenu.SetActive(true);
        options.SetActive(false);
        about.SetActive(false);
        Scenarios.SetActive(false); // Added
    }

    public void EnableOption()
    {
        mainMenu.SetActive(false);
        options.SetActive(true);
        about.SetActive(false);
    }
    public void EnableAbout()
    {
        mainMenu.SetActive(false);
        options.SetActive(false);
        about.SetActive(true);
    }

    public void EnableScenarios()
    {
        mainMenu.SetActive(false);
        options.SetActive(false);
        about.SetActive(false);
        Scenarios.SetActive(true);
    }

    public void LoadBLSScene()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(1);
    }

    public void LoadBulletScene()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(2);
    }

    public void LoadHeartScene()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(3);
    }

    public void LoadHypoglycemicScene()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(4);
    }


}
