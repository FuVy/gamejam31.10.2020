using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowRebel : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    Health health;
    PlayerMovement player;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float checkRadius = 8f;
    [SerializeField] LayerMask playerMask;  
    [SerializeField] Transform playerCheck;
    [SerializeField] GameObject gun;
    [SerializeField] bool lookingLeft;

    bool foundPlayer;
    bool canShoot;
    bool windowOpen;
    bool isDead;
    AudioSource audioSource;

    void Start()
    {
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        animator.SetBool("notDead", true);
        foundPlayer = false;
        canShoot = false;
        windowOpen = false;
        isDead = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!foundPlayer)  
        {
            foundPlayer = Physics2D.OverlapCircle(playerCheck.position, checkRadius, playerMask) && ((player.transform.position.x < transform.position.x && lookingLeft) || (player.transform.position.x > transform.position.x && !lookingLeft)) ;
        }
        else if (((player.transform.position.x < transform.position.x && lookingLeft) || (player.transform.position.x > transform.position.x && !lookingLeft)) && !isDead)
        //else if (!isDead)
        {
            if (!windowOpen)
            {
                animator.SetTrigger("Open");
                //canShoot = true;
                windowOpen = true;
            }
            if (canShoot)
            {
                animator.SetTrigger("Attack");
                canShoot = false;
                StartCoroutine(WaitBeforeAttack());
            }
        }
        else
            animator.SetTrigger("Die");
    }

    IEnumerator WaitBeforeAttack()
    {
        yield return new WaitForSeconds(Random.Range(0.8f, 1.4f));
        canShoot = true;
    }

    public void OpenWindow()
    {
        windowOpen = true;
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
        isDead = true;
    }
}
