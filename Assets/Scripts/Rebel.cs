using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebel : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    Health health;
    PlayerMovement player;
    [SerializeField] GameObject projectilePrefab;
    private readonly float checkRadius = 5f;
    [SerializeField] LayerMask playerMask;
    [SerializeField] Transform playerCheck;
    [SerializeField] GameObject gun;

    bool foundPlayer;
    bool canShoot;

    AudioSource audioSource;
    void Start()
    {
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        foundPlayer = false;
        canShoot = true;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!foundPlayer)
        {
            foundPlayer = Physics2D.OverlapCircle(playerCheck.position, checkRadius, playerMask);
        }
        else if (health.GetHealth() > 0)
            {
            gameObject.transform.rotation = player.transform.position.x < transform.position.x ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
            if (canShoot)
            {
                animator.SetTrigger("Attack");
                canShoot = false;
                StartCoroutine(WaitBeforeAttack());
            }
        }
        else
        {
            animator.SetTrigger("Die");
            gameObject.layer = LayerMask.NameToLayer("Down");
        }
    }

    IEnumerator WaitBeforeAttack()
    {
        yield return new WaitForSeconds(Random.Range(1.5f, 2f));
        canShoot = true;
    }

    public void SetCanShoot()
    {
        canShoot = true;
    }

    public void CreateBullet()
    {
        GameObject shotProjectile = Instantiate(projectilePrefab, gun.transform.position, gun.transform.rotation);
        audioSource.Play();
    }

    public void Die()
    {
        gameObject.layer = LayerMask.NameToLayer("Down");
    }
}
