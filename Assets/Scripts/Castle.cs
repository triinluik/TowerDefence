using UnityEngine;

public class Castle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        WaypointFollower enemy = collision.gameObject.GetComponent<WaypointFollower>();
        if (enemy != null)
        {
            Events.LifeLost();
            Destroy(enemy.gameObject);
        }
    }
}
