using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryHandler : MonoBehaviour
{
    [SerializeField] public Text textUI;

    [SerializeField] string[] textArray;

    public int whichText = 0;
    Animator animator;

    AudioSource audioSource;

    private void Start()
    {
        whichText = 0;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FindObjectOfType<LevelLoader>().LoadNextLevel();
        }
    }
    public void StartShowingText()
    {
        StartCoroutine(ShowText(textArray[whichText]));
    }

    public void NextText()
    {
        if (whichText < textArray.Length - 1)
        {
            whichText++;
            switch (whichText)
            {
                case 1:
                    animator.SetTrigger("startSecond");
                    break;
                case 2:
                    animator.SetTrigger("startThird");
                    break;
                case 3:
                    animator.SetTrigger("startFourth");
                    break;
                case 4:
                    animator.SetTrigger("startFifth");
                    break;
            }
            StartShowingText();
        }
    }

    IEnumerator ShowText(string text)
    {
        int i = 0;
        while (i <= text.Length)
        {
            textUI.text = text.Substring(0, i);
            i++;
            audioSource.Play();

            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(2f);

        if (whichText == textArray.Length - 1)
        {
            FindObjectOfType<LevelLoader>().LoadNextLevel();
        }
        NextText();
    }
}
