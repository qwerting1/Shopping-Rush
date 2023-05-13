using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreen : MonoBehaviour
{

    public AudioSource End;
    private TextMeshProUGUI endMessage;
    // Start is called before the first frame update
    void Start()
    {
        End.Play();
        endMessage = GameObject.Find("endMessage").GetComponent<TextMeshProUGUI>();
        endMessage.SetText("You completed " + ScoreManager.GetCompletedCount() + " lists!" + "\n" + "with a score of " + ScoreManager.GetScore());
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Restart()
    {
        End.Stop();
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        End.Stop();
        SceneManager.LoadScene(0);
    }
}
