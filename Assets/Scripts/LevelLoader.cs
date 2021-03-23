using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{


    [SerializeField] float timeToWait = 1.5f;

    IEnumerator WaitBeforeLoad()
    {
        FindObjectOfType<Animator>().SetTrigger("blackout");
        yield return new WaitForSeconds(timeToWait);
        LoadNextLevel();
    }
    IEnumerator WaitBeforeLoad(int index)
    {
        FindObjectOfType<Animator>().SetTrigger("blackout");
        yield return new WaitForSeconds(timeToWait);
        SceneManager.LoadScene(index);
    }

    public void LoadNextLevelWithWaiting()
    {
        StartCoroutine(WaitBeforeLoad());
    }
    public void LoadNextLevelWithWaiting(int index)
    {
        StartCoroutine(WaitBeforeLoad(index));
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
