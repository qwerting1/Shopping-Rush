using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    public AudioSource Game;

    private TextMeshProUGUI timerText;
    private float timeRemaining = 180.0f; // 3 minutes in seconds
    // Start is called before the first frame update
    void Start()
    {
        Game.Play();
        timerText = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the timer
        timeRemaining -= Time.deltaTime;

        // Convert timeRemaining to minutes and seconds
        int minutes = Mathf.FloorToInt(timeRemaining / 60.0f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60.0f);

        // Update the TMP Text
        timerText.SetText("Time: "+string.Format("{0:00}:{1:00}", minutes, seconds));

        // Check if time has run out
        if (timeRemaining <= 0.0f)
        {
            Game.Stop();
            // Time's up! Do something here, like end the game or trigger an event
            Debug.Log("Time's up!");
        }
    }

    // Call this mehtod to get the time remaining
    public float GetTimeRemaining()
    {
        return timeRemaining;
    }
}
