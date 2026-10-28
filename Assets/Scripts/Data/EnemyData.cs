using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/EnemyData")]
public class EnemyData : ScriptableObject
{
    public int EnemyHealth = 1;
    public int EnemyDamage = 1;
    public float EnemySpeed = 2f;
    public Sprite EnemySprite;
}

