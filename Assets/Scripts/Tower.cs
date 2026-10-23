using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public List<Health> enemiesInRange = new List<Health>();
    public Projectile ProjectilePrefab;
    public int Damage = 1;

    public float NextFireTime = 0;

    public float FireInterval = 2.0f;

    public void Update()
    {
        if (Time.time >= NextFireTime && enemiesInRange.Count != 0)
        {
            Fire();
            NextFireTime = Time.time + FireInterval;
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health enemy = collision.gameObject.GetComponent<Health>();
        //Does object have Health component? If yes then add to list.
        if (enemy != null)
        {
            enemiesInRange.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Health enemy = collision.gameObject.GetComponent<Health>();

        if (enemy != null)
        {
            enemiesInRange.Remove(enemy);
        }
    }

    private void Fire()
    {
        if (enemiesInRange.Count == 0)
        {
            return;
        }

        int random = Random.Range(0, enemiesInRange.Count);

        Health target = enemiesInRange[random];

        if (target == null)
        {
            enemiesInRange.RemoveAt(random);
            return;
        }
        Projectile projectile = Instantiate<Projectile>(ProjectilePrefab, transform.position, Quaternion.identity);
    
        projectile.Target = target;
        projectile.Damage = Damage;
    }
}
