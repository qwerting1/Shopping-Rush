using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreen : MonoBehaviour
{

    private TextMeshProUGUI endMessage;
    // Start is called before the first frame update
    void Start()
    {
        endMessage = GameObject.Find("endMessage").GetComponent<TextMeshProUGUI>();
        endMessage.SetText("You completed " + ScoreManager.GetCompletedCount() + " lists!" + "\n" + "with a score of " + ScoreManager.GetScore());
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnButtonPress()
    {
        SceneManager.LoadScene(1);
    }
}
