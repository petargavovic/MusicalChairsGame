using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject creditsPanel;
    public GameObject tutorialPanel;

    void Start()
    {
        if(!PlayerPrefs.HasKey("tutorial"))
        {
            tutorialPanel.SetActive(true);
            tutorialPanel.GetComponent<Animator>().Play("creditsPanel");
        }
    }

    void Update()
    {

    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Credits()
    {
        //        creditsPanel.SetActive(!creditsPanel.activeSelf);
        if(creditsPanel.transform.localScale.x == 0)
        creditsPanel.GetComponent<Animator>().Play("creditsPanel");
        else
            creditsPanel.GetComponent<Animator>().Play("-creditsPanel");

    }

    public void TutorialClose()
    {
        tutorialPanel.GetComponent<Animator>().Play("-creditsPanel");
        PlayerPrefs.SetInt("tutorial", 1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
