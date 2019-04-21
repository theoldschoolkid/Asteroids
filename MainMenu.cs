using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Initialized when main menu scene is loaded
/// </summary>
public class MainMenu : MonoBehaviour
{
   
   public void NewGame()                     // Runs when new Game button is clicked
    {
        AudioManager.Play(AudioClipName.PlayerShot); 
        SceneManager.LoadScene("scene1");   //load gameplay level
    }

    public void Quit()                     // Runs when Quit button is clicked
    {
        AudioManager.Play(AudioClipName.PlayerShot); 
        Application.Quit(); //Quit application
    }
}
