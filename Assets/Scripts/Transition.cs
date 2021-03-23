using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    int killedCivs = 0, savedCivs = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<DataKeeper>().Count(killedCivs, savedCivs);
        FindObjectOfType<LevelLoader>().LoadNextLevel();
    }

    public void AddKilled()
    {
        killedCivs++;
    }

    public void AddSaved()
    {
        savedCivs++;
    }
}
