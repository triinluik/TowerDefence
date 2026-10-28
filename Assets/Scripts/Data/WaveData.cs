using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Game/Wave")]
public class WaveData : ScriptableObject
{
    public int EnemyCount = 3;
    public EnemyData EnemyType;
    public float SpawnCooldown = 1f;
}
