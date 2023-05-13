using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuPanel;
    public GameObject GameUI;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        PauseMenuPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))//cancel is ESC
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            PauseMenuPanel.SetActive(true);
            GameUI.SetActive(false);
            Time.timeScale = 0f;
        }
    }

    public void ResumeGame()//resume button
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        PauseMenuPanel.SetActive(false);
        GameUI.SetActive(true);
        Time.timeScale = 1f;
    }    
    
    public void MainMenu()//main menu button
    {
        SceneManager.LoadScene(0);
    }
    
    public void Quit()//quit button
    {
        Application.Quit();
    }

    public void Restart()//quit button
    {
        SceneManager.LoadScene(1);
    }
}
