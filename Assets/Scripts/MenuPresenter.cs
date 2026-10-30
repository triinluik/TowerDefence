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
        Debug.Log("Exit clicked");
        Application.Quit();
    }

    public void StartScenario(int index) // No more duplicate code, you're welcome xd
    {
        selectedScenario = scenarios[index];
        SceneManager.LoadScene(selectedScenario.SceneName);
    }

    //Also, Jaanus wants OnLevelWasLoaded, Unity disagrees and warns it's deprecated haha
    public void OnLevelWasLoaded(int level)
    {
        Debug.Log("OnLevelWasLoaded called! Level: " + level);
        if (SceneManager.GetActiveScene().ToString() != "MainMenu")
        {
            // GetComponent gave us a NullReference, cause Unity xd
            ScenarioController controller = FindFirstObjectByType<ScenarioController>();
            controller.SetScenario(selectedScenario);
        }
        else 
        {
            gameObject.SetActive(true);
        }
    }

}
