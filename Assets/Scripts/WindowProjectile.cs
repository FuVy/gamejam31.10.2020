using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowProjectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 14f;
    [SerializeField] int damage = 100;
    Vector3 playerPosition; 


    private void Start()
    {
        Destroy(gameObject, 4f);
        playerPosition = FindObjectOfType<PlayerMovement>().transform.position;
        Vector3 vectorToTarget = (FindObjectOfType<PlayerMovement>().transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion qt = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, qt, Time.deltaTime * 1000000f);
        GetComponent<Rigidbody2D>().velocity = vectorToTarget * projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var health = collision.GetComponent<Health>();
        //var player = collision.GetComponent<PlayerMovement>();
        var civ = collision.GetComponent<Civilian>();
        
        if (civ)
        {
            bool fromRight = (collision.transform.position.x < transform.position.x);
            health.DealDamage(damage, fromRight);
        }
        else if (health)
        {
            health.DealDamage(damage);
        }
        Destroy(gameObject);
    }
}
