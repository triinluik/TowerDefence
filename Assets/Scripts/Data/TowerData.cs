using UnityEngine;

[CreateAssetMenu(menuName = "Game/Tower")]
public class TowerData : ScriptableObject
{
    //public Projectile projectilePrefab;
    //public int spawnCooldown; //not needed here, just in the tower script itself?

    public string Name;
    public int Cost;
    public string Hotkey;

    public Tower Prefab;
    public Sprite Icon;

}
