using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Health Target;
    public float Speed = 0.5f;
    public int Damage = 1;

    void Update()
    {
        if (Target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 targetPosition = Target.transform.position;

        Vector2 newPosition = Vector2.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);

        transform.position = new Vector3(newPosition.x, newPosition.y);

        // Tiny optimisation using sqrMagnitude vs magnitude
        if ((transform.position - targetPosition).sqrMagnitude < 0.01)
        {
            Target.Damage(Damage);
            Destroy(gameObject);
        }
    }
}
