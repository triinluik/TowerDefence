using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenarioController : MonoBehaviour
{
    public int Gold = 100;
    public int Lives = 10;

    public ScenarioData currentScenario;
    public TowerCard TowerCardPrefab;
    public Transform TowerPanel;

    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI LivesText;
    public TextMeshProUGUI LevelEndText;
    public GameObject GameOverPanel;
    public Spawn[] Spawner;
    private int currentWaveIndex = 0;

    public bool IsRunning { get; private set; } = true;

    public void SetScenario(ScenarioData scenario)
    {
        /* MenuPresenter passes the selected scenario
        SetScenario save it*/
        currentScenario = scenario;
        CreateTowerCards();
        // & StartLevel tells Spawner to begin the first wave
        StartLevel();
    }
    private void UpdateHUD()
    {
        GoldText.text = Gold.ToString();
        LivesText.text = Lives.ToString();
    }

    void Start()
    {
        // Initializes Gold and Lives based on the current scenario
        if (currentScenario != null)
        {
            Gold = currentScenario.StartingGold;
            Lives = currentScenario.Lives;
        }
        UpdateHUD();
    }

    /* Flow: Spawn finishes spawning enemies -> CheckWaveCompleted() -> wait if enemies remain
     * Last enemy disappears -> OnDestroy() -> CheckWaveCompleted() -> WaveCompleted() -> next wave/victory 
     */
    public void CheckWaveCompleted()
    {
        if (!IsRunning || currentScenario == null) return;
        foreach (Spawn spawner in Spawner)
        {
            if (!spawner.HasFinishedSpawning)
            {
                return;
            }
        }
        if (WaypointFollower.EnemyCounter > 0) return;
        if (Spawner.Length == 0) return;

        WaveCompleted();
    }
    public void WaveCompleted()
    {
        currentWaveIndex++;
        if (currentWaveIndex >= currentScenario.Waves.Length)
        {
            WinLevel();
            return;
        }
        foreach (Spawn spawner in Spawner)
        {
            spawner.StartWave(currentScenario.Waves[currentWaveIndex]);
        }
    }

    private void CreateTowerCards()
    {
        foreach (TowerData tower in currentScenario.Towers)
        {
            TowerCard card = Instantiate(TowerCardPrefab, TowerPanel);
            card.scenarioController = this;
            card.SetData(tower);
        }
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

    public void LoseLife(int damage)
    {
        Lives -= damage;
        UpdateHUD();

        if (Lives <= 0)
        {
            LoseLevel();
        }
    }
    public void LoseLevel()
    {
        if (!IsRunning) return;
        IsRunning = false;
        LevelEndText.text = "You Lose!";
        GameOverPanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void WinLevel()
    {
        if (!IsRunning) return;
        IsRunning = false;
        LevelEndText.text = "You Win!";
        GameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void StartLevel()
    {
        currentWaveIndex = 0;
        if (currentScenario != null && currentScenario.Waves.Length > 0)
        {
            foreach (Spawn spawner in Spawner)
            {
                spawner.StartWave(currentScenario.Waves[currentWaveIndex]);
            }
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; // Resume the game
        MenuPresenter.Instance.StartScenario(MenuPresenter.Instance.scenarios.IndexOf(currentScenario));
    }
}
