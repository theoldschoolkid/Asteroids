using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The script is attached to PauseMenu Object    
/// The script runs when "PauseMenu" is initialized by "CheckForPause" script
/// </summary>
public class PauseManager : MonoBehaviour
{

    GameObject UI;

    void Start() // Runs on initialization
    {
        Time.timeScale = 0;                             //Freeze the game
        UI = GameObject.FindGameObjectWithTag("UI");    // Find the gameplayUI object and disable it
        UI.GetComponent<UI>().running = false;
        LockCursor(false);
    }

    public void OnClickResume() // When Resumen button is clicked which is part of pause menu, this function runs
    {
        AudioManager.Play(AudioClipName.PlayerShot); // Play  sound
        Time.timeScale = 1;                          // Unfreeze the game
        this.gameObject.SetActive(false);            // Disable this gameobject and enable the gameplayUI
        UI.GetComponent<UI>().running = true;
        LockCursor(true);
        CheckForPause.Paused = false;
    }

    public void OnClickQuitToMenu() // When Quit button is clicked
    {
        AudioManager.Play(AudioClipName.PlayerShot); // Play sound
        Time.timeScale = 1;                          // Unfreeze the game
        SceneManager.LoadScene("MainMenu");
        LockCursor(false);
    }
    public void OnClickRetry()
    {
        print("iside");
        AudioManager.Play(AudioClipName.PlayerShot);
        Time.timeScale = 1;
        SceneManager.LoadScene("Scene1");   // Load gameplay level from start
        CheckForPause.Paused = false;

    }

    // function to enable and disable cursor
    public void LockCursor(bool isLocked)
    {
        if (isLocked)
        {
            // make the mouse pointer invisible
            Cursor.visible = false;

            // lock the mouse pointer within the game area
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            // make the mouse pointer visible
            Cursor.visible = true;

            // unlock the mouse pointer so player can click on other windows
            Cursor.lockState = CursorLockMode.None;
        }
    }

}
