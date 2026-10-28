using UnityEngine;

[CreateAssetMenu(menuName = "Game/Scenario")]
public class ScenarioData : ScriptableObject
{
    public string PresentedName;
    public string SceneName;
    public int Lives;
    public int StartingGold;

    public WaveData[] Waves;
}
