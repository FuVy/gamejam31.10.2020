using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Civilian civilian = collision.GetComponent<Civilian>();
        if (collision.GetComponent<Civilian>())
        {
            collision.GetComponent<Animator>().SetTrigger("fadeAway");
            civilian.DontMove();
        }
    }
}
