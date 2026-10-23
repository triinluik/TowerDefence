using UnityEngine;
[RequireComponent (typeof(Waypoint))]
public class Spawn : MonoBehaviour
{

    public WaypointFollower FollowerPrefab;

    public int EnemiesPerWave = 5;
    public float SpawnDelay = 0.5f;

    public float TimeBetweenWaves = 5.0f;
    private float NextSpawnTime = 0;

    private float NextWaveTime = 0;
    private int enemiesSpawnedInWave = 0;
    private bool waveFinished = false;
    void Start()
    {
        waveFinished = true;
        NextWaveTime = Time.time + 5f;
    }

    void Update()
    {
        if (waveFinished)
        {
            if (Time.time >= NextWaveTime)
            {
                StartNextWave();
            }
            return;
        }
        if (NextSpawnTime <= Time.time)
        {
            WaypointFollower follower = Instantiate<WaypointFollower>(FollowerPrefab, transform.position, Quaternion.identity);
            follower.NextWaypoint = GetComponent<Waypoint>();

            follower.NextWaypoint = GetComponent<Waypoint>();
            enemiesSpawnedInWave++;
            NextSpawnTime = SpawnDelay + Time.time;

            if (enemiesSpawnedInWave >= EnemiesPerWave)
            {
                waveFinished = true;
                NextWaveTime = Time.time + TimeBetweenWaves;
            }
        }
    }
    public void StartNextWave()
    {
        waveFinished = false;
        enemiesSpawnedInWave = 0;
        NextSpawnTime = Time.time;
    }
}
