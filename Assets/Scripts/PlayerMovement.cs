using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    Health health;

    [Header("Movement")]
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float jumpForce = 8f;
    float moveInput;

    [Header("Detecting Ground")]
    private readonly float checkRadius = .1f;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] Transform groundCheck;

    [Header("Projectile")]
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.2f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject spookyBulletPrefab;
    [SerializeField] GameObject gun;
    [SerializeField] GameObject spookyGun;

    bool ableToShoot = true;
    bool ableToMove = true;
    bool isGrounded;

    AudioSource audioSource;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if (ableToMove)
        {
            moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * movementSpeed, rb.velocity.y);
        }

        HandleAnimations(); 

        if (!Mathf.Approximately(0, moveInput))
            transform.rotation = moveInput < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;     //поворот в сторону движения
        if (health.GetHealth() <= 0)
        {
            animator.SetTrigger("playerDead");
            StartCoroutine(WaitBeforeRestart());
        }
    }

    IEnumerator WaitBeforeRestart()
    {
        yield return new WaitForSeconds(1f);
        FindObjectOfType<LevelLoader>().RestartScene();
    }

    private void HandleAnimations()
    {
        if (Mathf.Abs(rb.velocity.x) > 0.002f)        
            animator.SetBool("isWalking", true);
        else
            animator.SetBool("isWalking", false);

        if (rb.velocity.y > 0.05f)
        {
            animator.SetBool("isJumping", true);
            animator.SetBool("isFalling", false);
        }
        else if (rb.velocity.y < -0.05f)
        {
            if (rb.velocity.y > -1.5f)
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - .3f);
            animator.SetBool("isFalling", true);
            animator.SetBool("isJumping", false);
        }
        else
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }
    }

    private void Update()
    {
        if (ableToMove)                 
        {
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.velocity = Vector2.up * jumpForce;
            }
            
            if (Input.GetButtonDown("Fire1") && ableToShoot)
            {
                animator.SetBool("isShooting", true);
                ableToShoot = false;
            }
            if (Input.GetButtonUp("Fire1"))
            {
                animator.SetBool("isShooting", false);
                ableToShoot = true;                     //чтобы нельзя было бесконечно запускать корутин, после альт-таба
            }
            if (Input.GetButtonDown("Fire2"))
            {
                animator.SetTrigger("scare");
            }
        }
    }

    public void Fire()
    {
        GameObject shotProjectile = Instantiate(projectilePrefab, gun.transform.position, gun.transform.rotation);
        audioSource.Play();
        //Instantiate(projectilePrefab, gun.transform.position, gun.transform.rotation);
    }

    public void UnableToMove()
    {
        ableToMove = false;
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    public void AbleToMove()
    {
        ableToMove = true;
    }

    public void Scare()
    {
        GameObject shotProjectile = Instantiate(spookyBulletPrefab, spookyGun.transform.position, Quaternion.Euler(0, 0, 90));
        audioSource.Play();
    }
}
