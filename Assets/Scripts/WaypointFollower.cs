using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Waypoint NextWaypoint;
    public float Speed = 3.0f;
    public static int EnemyCounter = 0;
    public int CastleDamage = 1;

    void Start()
    {
        EnemyCounter++;
    }

    // Update is called once per frame
    void Update()
    {
        if (NextWaypoint == null)
        {
            ReachCastle();
            return;
        }

        Vector3 targetPosition = NextWaypoint.transform.position;

        Vector2 newPosition = Vector2.MoveTowards(transform.position, NextWaypoint.transform.position, Speed * Time.deltaTime);

        transform.position = new Vector3(newPosition.x, newPosition.y);

        // Tiny optimisation using sqrMagnitude vs magnitude
        if ((transform.position - targetPosition).sqrMagnitude < 0.01)
        {
            NextWaypoint = NextWaypoint.GetNextWaypoint();
        }
    }

    public void ReachCastle()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        EnemyCounter--;
    }
}
