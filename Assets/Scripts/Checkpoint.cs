using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<DataKeeper>().SetStartPos();
        Camera.main.transform.position = FindObjectOfType<PlayerMovement>().transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<DataKeeper>().SaveCheckpoint(transform.position.x, transform.position.y);
        GetComponent<Animator>().SetBool("isOn", true);
    }
    //чекпоинты вырезаны, сорян
}
