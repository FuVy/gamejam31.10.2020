using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpookyBullet : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 6f;
    void Start()
    {
        Destroy(gameObject, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * projectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Civilian civilian = collision.GetComponent<Civilian>();
        if (civilian)
        {
            civilian.GetScared();
        }
    }
}
