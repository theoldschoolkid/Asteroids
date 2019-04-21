using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForPause : MonoBehaviour
{
    [SerializeField]
    GameObject PauseMenu;

   public static bool Paused;

    GameObject UI;

    private void Start()
    {
        Paused = false;
        PauseMenu.GetComponent<PauseManager>().LockCursor(true); // Lock cursor at the start of the game
        UI = GameObject.FindGameObjectWithTag("UI");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    //called when spaceship is destroyed
    public void CallPauseMenu()
    {
        Invoke("PauseGame", 2f);
    }


    void PauseGame()
    {
        if (!Paused)
        {
            
            PauseMenu.SetActive(true);
            Paused = true;
            Time.timeScale = 0;                             //Freeze the game               
            UI.GetComponent<UI>().running = false;
            PauseMenu.GetComponent<PauseManager>().LockCursor(false);
        }
        else
        {
            PauseMenu.SetActive(false);
            UI.GetComponent<UI>().running = true;
            Time.timeScale = 1;
            Paused = false;
        }
    }

}