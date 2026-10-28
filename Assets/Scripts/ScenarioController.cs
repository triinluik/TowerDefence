using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenarioController : MonoBehaviour
{
    public int Gold = 100;
    public int Lives = 10;

    public ScenarioData currentScenario;

    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI LivesText;
    public TextMeshProUGUI LevelEndText;
    public GameObject GameOverPanel;

    public bool IsRunning { get; private set; } = true;

    private void UpdateHUD()
    {
        GoldText.text = Gold.ToString();
        LivesText.text = Lives.ToString();
    }

    void Start()
    {
        UpdateHUD();
    }
    private void OnEnable()
    {
        Events.OnEnemyKilled += AddGold;
        Events.OnLifeLost += LoseLife;
    }
    private void OnDisable()
    {
        Events.OnEnemyKilled -= AddGold;
        Events.OnLifeLost -= LoseLife;
    }
    public void AddGold(int amount)
    {
        Gold += amount;
        UpdateHUD();
    }

    public bool SpendGold(int amount)
    {
        if (Gold < amount)
        {
            return false;
        }
        Gold -= amount;
        UpdateHUD();
        return true;
    }

    public void AddLives(int amount)
    {
        Lives += amount;
        UpdateHUD();
    }

    public void LoseLife()
    {
        Lives--;
        UpdateHUD();
        if (Lives <= 0)
        {
            LoseLevel();
        }
    }
    public void LoseLevel()
    {
        if (!IsRunning) return;
        LevelEndText.text = "You Lose!";
        GameOverPanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void WinLevel()
    {
        LevelEndText.text = "You Win!";
        GameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void StartLevel()
    {
        Debug.Log("Level Started!");
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
