using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataKeeper : MonoBehaviour
{
    float startX, startY;
    int lastLevel;
    [SerializeField] int killedCivs = 0, savedCivs = 0;
    AudioSource audioSource;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameController"); 
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void SetStartPos()           //not used
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        Debug.Log(lastLevel);
        if (lastLevel == SceneManager.GetActiveScene().buildIndex)
        {
            FindObjectOfType<PlayerMovement>().transform.position = new Vector3(startX, startY, 0);
        }
    }

    public void Count(int killedCivs, int savedCivs)
    {
        this.killedCivs += killedCivs;
        this.savedCivs += savedCivs;
    }

    public void SaveCheckpoint(float x, float y)        //not used
    {
        startX = x;
        startY = y;
        lastLevel = SceneManager.GetActiveScene().buildIndex;
    }

    public int GetKilledCivs()
    {
        return killedCivs;
    }

    public int GetSavedCivs()
    {
        return savedCivs;
    }
    

}
