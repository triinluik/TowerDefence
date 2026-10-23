using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerCard : MonoBehaviour
{
    public TowerData data;

    public ScenarioController scenarioController;

    public Image Icon;
    public TextMeshProUGUI CostText;
    public TextMeshProUGUI HotkeyText;

    private Button button;

    private void Update()
    {
        button.interactable = scenarioController.Gold >= data.Cost;

        if (Input.GetKeyDown(KeyCode.Alpha1) && data.Hotkey == "1")
            Pressed();
        if (Input.GetKeyDown(KeyCode.Alpha2) && data.Hotkey == "2")
            Pressed();
        if (Input.GetKeyDown(KeyCode.Alpha3) && data.Hotkey == "3")
            Pressed();
    }

    void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(Pressed);
        }
        SetData(data);
    }

    public void SetData(TowerData data)
    {
        Icon.sprite = data.Icon;
        CostText.text = data.Cost.ToString();
        HotkeyText.text = data.Hotkey;
    }

    public void Pressed()
    {
        Events.SelectTower(data);
    }

}
