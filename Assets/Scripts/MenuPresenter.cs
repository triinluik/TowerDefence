using System.Collections.Generic;
using Unity.AppUI.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPresenter : MonoBehaviour
{
    public static MenuPresenter Instance;

    public List<ScenarioData> scenarios;
    public ScenarioData selectedScenario;
    public Button exitButton;

    // Two levels only (thank god xd)

    private void Awake() //Making sure there is only one instance
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartScenario1() //duplicate code, cause Im lazy xd
    {
        SceneManager.LoadScene("LevelOne");
        selectedScenario = scenarios[0];
        OnLevelWasLoaded(1);
    }

    public void StartScenario2()
    {
        SceneManager.LoadScene("LevelTwo");
        selectedScenario = scenarios[2];
        OnLevelWasLoaded(2);
    }

    public void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().ToString() != "MainMenu")
        {
            gameObject.SetActive(false);
            GetComponent<ScenarioController>().currentScenario = selectedScenario;
        }
        else 
        {
            gameObject.SetActive(true);
        }
    }

}
