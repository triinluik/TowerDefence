using System;

public static class Events
{
    public static event Action<TowerData> OnTowerSelected;
    public static event Action<int> OnEnemyKilled;
    public static event Action OnLifeLost;

    // This method is used to invoke the OnTowerSelected event when a tower is selected.
    public static void SelectTower(TowerData data) => OnTowerSelected?.Invoke(data);

    //Invoke OnEnemyKilled event when an enemy is killed.
    public static void EnemyKilled(int goldReward) => OnEnemyKilled?.Invoke(goldReward);

    //Invoke OnLifeLost event when a life is lost.
    public static void LifeLost() => OnLifeLost?.Invoke();
}
