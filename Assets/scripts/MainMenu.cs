using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource MainMenuM;
    public GameObject ControlsPanel;
    // Start is called before the first frame update
    void Start()
    {
        MainMenuM.Play();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        ControlsPanel.SetActive(false);
    }

    public void Play()
    {
        MainMenuM.Stop();
        SceneManager.LoadScene(1);
    }

    public void ControlsPage()
    {
        ControlsPanel.SetActive(true);
    }

    public void ControlsPageBack()
    {
        ControlsPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
