using UnityEngine;

[CreateAssetMenu(menuName = "Game/Tower")]
public class TowerData : ScriptableObject
{
    public string Name;
    public int Cost;
    public string Hotkey;

    public Tower Prefab;
    public Sprite Icon;

}
