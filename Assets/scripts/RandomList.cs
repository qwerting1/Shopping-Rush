using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomList : MonoBehaviour
{
    public List<string> wordList;

    private int score = 0;
    private TMP_Text wordText;
    private TMP_Text wordTextScore;
    private List<string> RandomWordList;
    private string itemTag; //tag from item
    void Start()
    {
        var canvas = GameObject.Find("Canvas");
        var textTransform = canvas.transform.Find("List (TMP)");
        wordText = textTransform.GetComponent<TMP_Text>();

        var canvasScore = GameObject.Find("Canvas");
        var textTransformScore = canvasScore.transform.Find("Score (TMP)");
        wordTextScore = textTransformScore.GetComponent<TMP_Text>();

        RandomWordList = new List<string>();
        List<string> tempWordList = new List<string>(wordList);
        for (int i = 0; i < 5; i++)
        {
            if (tempWordList.Count == 0)
            {
                // All words have been picked, exit the loop
                break;
            }
            int randomIndex = Random.Range(0, tempWordList.Count);
            RandomWordList.Add(tempWordList[randomIndex]);
            tempWordList.RemoveAt(randomIndex);
        }
        wordText.text = string.Join("\n", RandomWordList.ToArray());
    }

    public void ReceiveTag(string tag)
    {
        itemTag = tag;
        Debug.Log("Received tag: " + tag); //recieve the item's tag from the script itemPickup.cs
        if (RandomWordList.Contains(tag)) 
        {
        score += 1;
        }
        RandomWordList.Remove(itemTag); //remove the item from the list
        wordText.text = string.Join("\n", RandomWordList.ToArray()); // update the list
        wordTextScore.text = "Score: " + score;
    }
}
