using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreen : MonoBehaviour
{

    private TextMeshProUGUI endMessage;
    private ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        endMessage = GameObject.Find("endMessage").GetComponent<TextMeshProUGUI>();
        endMessage.SetText("You completed " + scoreManager.GetCompletedCount() + " lists!" + "\n" + "with a score of " + scoreManager.GetScore());
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    //// Update is called once per frame
    //void Update()
    //{
      
    //}

    public void OnButtonPress()
    {
        Debug.Log("Button clicked times.");
        SceneManager.LoadScene(1);
    }
}
