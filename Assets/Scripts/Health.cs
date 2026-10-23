using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 3;
    public int goldReward = 5;

    public void Damage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            // Notify that the enemy has been killed and provide the gold reward
            Events.EnemyKilled(goldReward);
            Destroy(gameObject);
        }
    }
}
