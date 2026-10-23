using UnityEngine;

[CreateAssetMenu(menuName = "TowerDefence/Scenario")]
public class ScenarioData : ScriptableObject
{
    public string PresentedName;
    public string SceneName;
    public int Lives;
    public int StartingGold;

    public WaveData[] Waves;
}
