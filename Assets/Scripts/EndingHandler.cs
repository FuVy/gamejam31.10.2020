using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingHandler : MonoBehaviour
{
    [SerializeField] public Text textUI;

    [SerializeField] string[] textArray;

    public int whichText = 0;
    Animator animator;

    DataKeeper data;
    AudioSource audioSource;

    private void Start()
    {
        whichText = 0;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        data = FindObjectOfType<DataKeeper>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FindObjectOfType<LevelLoader>().LoadMainMenu();
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
                    {
                        if (data.GetKilledCivs() >= 6)
                            textArray[1] = "Стоит вам только появиться в каком-то месте, так сразу увеличивается число мёртвых гражданских. Совпадение?";
                        else if (data.GetKilledCivs() == 0)
                        {
                            textArray[1] = "Благодаря вашим действиям ни один человек не пострадал, а вы получили особую награду от международного сообщества";
                        }
                        else if (data.GetKilledCivs() > 0)
                        {
                            textArray[1] = "Хоть гражданские вам и не доверяют, но вы же спасли пару жизней, правда?";
                        }
                        //else
                        //{
                        //    textArray[1] = "Международное сообщество благодарно вам за ваши подвиги";
                        //}
                        animator.SetTrigger("startSecond");
                        break;
                    }
                case 2:
                    {
                        animator.SetTrigger("startThird");
                        break;
                    }
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
            FindObjectOfType<LevelLoader>().LoadMainMenu();
        }
        NextText();
    }
}
