using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    Health health;
    PlayerMovement player;

    private readonly float checkRadius = .4f;
    [SerializeField] LayerMask playerMask;
    [SerializeField] Transform playerCheck;

    [SerializeField] int damage = 50;
    float speed = 4f;

    bool isFollowing;
    bool canBash;
    bool isActive;
    void Start()
    {
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();    
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        isFollowing = true;
        canBash = true;
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (player.transform.position.x > transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 00, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            if (isFollowing)
            {
                animator.SetBool("isRunning", true);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), speed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }

            if (health.GetHealth() < 1000)
            {
                speed = 6f;
                if (canBash)
                {
                    Bash();
                }
                //StartCoroutine(WaitBeforeNextBash());
            }

            if (Physics2D.OverlapCircle(playerCheck.position, checkRadius, playerMask))
            {
                Attack();
            }
        }
    }

    IEnumerator WaitBeforeNextBash()
    {
        yield return new WaitForSeconds(5f);
        canBash = true;
    }

    public void Bash()
    {
        if (isActive)
        {
            animator.SetTrigger("Bash");
            isFollowing = false;
            canBash = false;
            if (player.transform.position.x > transform.position.x)
            {
                rb.velocity = Vector2.right * 5f;
            }
            else
            {
                rb.velocity = Vector2.left * 5f;
            }
            StartCoroutine(WaitBeforeNextBash());
        }
    }
    
    public void SetFollowing()
    {
        isFollowing = true;
    }
    

    public void Attack()
    {
        animator.SetTrigger("Attack");
        isFollowing = false;
    }

    public void CheckIfInArea()
    {
        if (Physics2D.OverlapCircle(playerCheck.position, checkRadius, playerMask))
        {
            player.gameObject.GetComponent<Health>().DealDamage(damage);
        }
    }

    public void SetIsInActive()
    {
        isActive = false;
        animator.SetBool("isRunning", false);

        //FindObjectOfType<LevelLoader>().GetComponent<Animator>().SetTrigger("blackout");
        //FindObjectOfType<LevelLoader>().LoadNextLevelWithWaiting();                           //убрать комментарий
        FindObjectOfType<LevelLoader>().LoadNextLevelWithWaiting(6); 
    }
}
