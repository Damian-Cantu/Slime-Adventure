using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
public class MainMenu : MonoBehaviour
{
    public DataSaving ds;

    public GameObject playButton;
    public GameObject text;
    public GameObject exitButton;
    public GameObject tutorial;
    public GameObject tutorialPlay;
    public void PlayGame()
    {
        string health = "12";
        string potions = "0";
        string coins = "0";
        string file = @"Assets\Characters\Player\Player_Data.txt";
        string[] textLines1 = {health, potions, coins};
        File.WriteAllLines(file, textLines1);
        ds.SaveData(12f, 0f, 0f, 0f, 0f);

        SceneManager.LoadScene("Plains", LoadSceneMode.Single);
    }

    public void GoToTutorial()
    {
        playButton.SetActive(false);
        text.SetActive(false);
        exitButton.SetActive(false);

        tutorial.SetActive(true);
        tutorialPlay.SetActive(true);

    }

    public void StartMenu()
    {       
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
