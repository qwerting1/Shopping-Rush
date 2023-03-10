using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomList : MonoBehaviour
{
    public List<string> wordList;//all products in shop
    public GameObject beans;

    private int score = 0;

    private TextMeshProUGUI listTextUI1;
    private TextMeshProUGUI listTextUI2;
    private TextMeshProUGUI listTextUI3;

    private TextMeshProUGUI scoreTextUI;

    private List<string> RandomWordList1;
    private List<string> RandomWordList2;
    private List<string> RandomWordList3;

    private string itemTag; //tag from item
    void Start()
    {
        listTextUI1 = GameObject.Find("List1").GetComponent<TextMeshProUGUI>();
        listTextUI2 = GameObject.Find("List2").GetComponent<TextMeshProUGUI>();
        listTextUI3 = GameObject.Find("List3").GetComponent<TextMeshProUGUI>();

        scoreTextUI = GameObject.Find("Score (TMP)").GetComponent<TextMeshProUGUI>();
        GenerateList1();//make list on the first text box in UI
        GenerateList2();//make list on the second text box in UI
        GenerateList3();//make list on the third text box in UI
    }

    private void Update()
    {
        listTextUI1.SetText(string.Join("\n", RandomWordList1.ToArray()));
        listTextUI2.SetText(string.Join("\n", RandomWordList2.ToArray()));
        listTextUI3.SetText(string.Join("\n", RandomWordList3.ToArray()));
    }

    public void ReceiveTag(string tag)
    {
        itemTag = tag.ToString();

        if (RandomWordList1.Contains(itemTag))
        {
            score += 1;
            RandomWordList1.Remove(itemTag); //remove the item from the list
        }
        else if (RandomWordList2.Contains(itemTag))
        {
            score += 1;
            RandomWordList2.Remove(itemTag); //remove the item from the list
        }
        else if (RandomWordList3.Contains(itemTag))
        {
            score += 1;
            RandomWordList3.Remove(itemTag); //remove the item from the list
        }
        else
        {
            score -= 1;
            Instantiate(beans, GameObject.Find("InstantiatePunishLocation").GetComponent<Transform>().transform.position, GameObject.Find("InstantiatePunishLocation").GetComponent<Transform>().transform.rotation);
            print("punish complete");
        }


        //listTextUI.text = string.Join("\n", RandomWordList.ToArray()); // update the list
        scoreTextUI.SetText("Score " + score);
        //scoreTextUI.text = "Score: " + score;
    }

    public void GenerateList1()
    {
        RandomWordList1 = new List<string>();
        List<string> tempWordList = new List<string>(wordList);
        for (int i = 0; i < 5; i++)
        {
            if (tempWordList.Count == 0)
            {
                // All words have been picked, exit the loop
                break;
            }
            int randomIndex = Random.Range(0, tempWordList.Count);
            RandomWordList1.Add(tempWordList[randomIndex]);
            tempWordList.RemoveAt(randomIndex);
            listTextUI1.SetText(string.Join("\n", RandomWordList1.ToArray()));
        }
    }
    public void GenerateList2()
    {
        RandomWordList2 = new List<string>();
        List<string> tempWordList = new List<string>(wordList);
        for (int i = 0; i < 5; i++)
        {
            if (tempWordList.Count == 0)
            {
                // All words have been picked, exit the loop
                break;
            }
            int randomIndex = Random.Range(0, tempWordList.Count);
            RandomWordList2.Add(tempWordList[randomIndex]);
            tempWordList.RemoveAt(randomIndex);
            listTextUI2.SetText(string.Join("\n", RandomWordList2.ToArray()));
        }
    }
    public void GenerateList3()
    {
        RandomWordList3 = new List<string>();
        List<string> tempWordList = new List<string>(wordList);
        for (int i = 0; i < 5; i++)
        {
            if (tempWordList.Count == 0)
            {
                // All words have been picked, exit the loop
                break;
            }
            int randomIndex = Random.Range(0, tempWordList.Count);
            RandomWordList3.Add(tempWordList[randomIndex]);
            tempWordList.RemoveAt(randomIndex);
            listTextUI3.SetText(string.Join("\n", RandomWordList3.ToArray()));
        }
    }
}
