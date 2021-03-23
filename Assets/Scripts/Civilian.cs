using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civilian : MonoBehaviour
{
    bool isScared;
    [SerializeField] GameObject house;
    [SerializeField] float oldPosition;
    [SerializeField] int timesBeforeDestroy = 2;
    Quaternion oldEuler;
    Animator animator;
    bool isInteractable;
    bool isGoingBack;
    DataKeeper data;
    

    void Start()
    {
        isScared = false;
        isInteractable = true;
        isGoingBack = false;
        animator = GetComponent<Animator>();
        data = FindObjectOfType<DataKeeper>();
        oldPosition = transform.position.x;
        oldEuler = transform.rotation;
    }

    void Update()
    {
        if (isScared)
        {
            //transform.Translate(Vector2.right * 6f * Time.deltaTime);
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(house.transform.position.x, transform.position.y), 6f * Time.deltaTime);
            animator.SetBool("isRunning", true);
        }
        else if (isGoingBack)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(oldPosition, transform.position.y), 3f * Time.deltaTime);
            animator.SetBool("isRunning", true);
            if (transform.position.x == oldPosition)
            {
                isGoingBack = false;
                transform.rotation = oldEuler;
                //isGoingBack = false;
            }
        }
        else if (GetComponent<Rigidbody2D>().velocity.x >= 0.02f)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    public void GetScared()
    {
        //if (isInteractable)
        //{
            isScared = true;
            transform.rotation = house.transform.position.x < transform.position.x ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
        //}
    }

    public void DontMove()
    {
        isScared = false;
        //isInteractable = false;
        gameObject.layer = LayerMask.NameToLayer("Down");
        isGoingBack = false;
    }

    public void GotInHouse()
    {
        //data.AddKarma(3);
        //gameObject.SetActive(false);
        if (timesBeforeDestroy > 0)
        {
            gameObject.layer = LayerMask.NameToLayer("Down");
            StartCoroutine(GetOutside());
            timesBeforeDestroy -= 1;
        }
        else
        {
            Destroy(gameObject);
            //data.AddKarma(4);
            FindObjectOfType<Transition>().AddSaved();
        }
        
    }

    IEnumerator GetOutside()
    {
        yield return new WaitForSeconds(Random.Range(3f,5f));
        animator.SetTrigger("getOutside");
        StartCoroutine(GetFromHouse());
    }

    public void HasDied()
    {
        //data.AddKarma(-6);
        FindObjectOfType<Transition>().AddKilled();
    }
    
    IEnumerator GetFromHouse()
    {
        isGoingBack = true;
        /*
        if (Random.Range(0, 2) == 1)
            GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(4f ,6f), GetComponent<Rigidbody2D>().velocity.y);
        else
            GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-4f, -6f), GetComponent<Rigidbody2D>().velocity.y);
        */
        transform.rotation = oldPosition < transform.position.x ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
        yield return new WaitForSeconds(1f);
        gameObject.layer = LayerMask.NameToLayer("Civilian");

    }
}
