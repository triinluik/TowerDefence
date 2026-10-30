using UnityEngine;
[RequireComponent (typeof(Waypoint))]
public class Spawn : MonoBehaviour
{

    public WaypointFollower FollowerPrefab;
    public ScenarioController ScenarioController;

    private WaveData currentWave;
    private float nextSpawnTime;
    private int enemiesSpawnedInWave = 0;
    private bool waveFinished = true;
    // Allows ScenarioController to check if the wave has finished spawning but not modify the value
    public bool HasFinishedSpawning => waveFinished;

    void Update()
    {
        if (waveFinished || currentWave == null) return;

        if (Time.time < nextSpawnTime) return;

        WaypointFollower follower = Instantiate(FollowerPrefab, transform.position, Quaternion.identity);

        // One prefab, multiple enemy types. Not gonna build an entire army
        // Assuming that's what you had in mind :D
        follower.NextWaypoint = GetComponent<Waypoint>();
        follower.Speed = currentWave.EnemyType.EnemySpeed;
        follower.CastleDamage = currentWave.EnemyType.EnemyDamage;

        Health enemyHealth = follower.GetComponent<Health>();
        enemyHealth.health = currentWave.EnemyType.EnemyHealth;

        SpriteRenderer spriteRenderer = follower.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = currentWave.EnemyType.EnemySprite;

        enemiesSpawnedInWave++;
        nextSpawnTime = Time.time + currentWave.SpawnCooldown;

        if (enemiesSpawnedInWave >= currentWave.EnemyCount)
        {
            waveFinished = true;
            // Stops Unity from calling Update() when there's no more enemies to spawn
            enabled = false;

            // Notify that the wave has finished
            ScenarioController.CheckWaveCompleted();
        }
    }
    public void StartWave(WaveData wave)
    {
        Debug.Log($"StartWave called on {gameObject.name} with {wave.EnemyCount} enemies");
        currentWave = wave;
        waveFinished = false;
        enemiesSpawnedInWave = 0;
        nextSpawnTime = Time.time;
        // Disabled in Inspector to prevent spawning before the first wave starts
        enabled = true; 
    }
}
