﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverMenu : MonoBehaviour
{
   
    public void PlayAgain()
    {
        SceneManager.LoadScene("Level1");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Ending1()
    {
        SceneManager.LoadScene("Ending 1");//ending 1
    }
    public void Ending2()
    {
        SceneManager.LoadScene("Ending 2");//ending 2
    }
}
