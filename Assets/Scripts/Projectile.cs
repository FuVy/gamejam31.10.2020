using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 6f;
    [SerializeField] int damage = 0;

    private void Start()
    {
        Destroy(gameObject, 4f);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * projectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var health = collision.GetComponent<Health>();
        var enemy = collision.GetComponent<WindowRebel>();
        var civilian = collision.GetComponent<Civilian>();
        if (health)
        {
            bool fromRight = (collision.transform.position.x < transform.position.x);
            health.DealDamage(damage, fromRight);
        }
        if (enemy && health.GetHealth() > 0)
        {
            health.DealDamage(damage);
        }
        Destroy(gameObject);
    }
}
