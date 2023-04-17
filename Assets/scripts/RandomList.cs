using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public enum ShoppingListStatus
{
    New,
    Active,
    Completed,
    Failed,
    Old
}

public class ShoppingList
{
    public ShoppingListStatus Status = ShoppingListStatus.New;
    public List<string> Items = new List<string>();
    public float StartTime;
    public float TimeLimit;

    public ShoppingList(List<string> wordList, float TimeLimit = 30)
    {
        this.Status = ShoppingListStatus.Active;
        this.TimeLimit = TimeLimit;
        this.StartTime = Time.time;
        this.Items = GenerateList(wordList);
    }

    public bool IsTimeUp() { 
        return Time.time > this.StartTime + this.TimeLimit;
    }
    public float GetTimeLeft()
    {
        return (this.StartTime + this.TimeLimit) - Time.time;
    }

    public bool RemoveItem(string item)
    {
        return this.Items.Remove(item);
    }

    public string FormattedList()
    {
        if(this.Status == ShoppingListStatus.Completed)
        {
            return "Completed list";
        }
        else if(this.Status == ShoppingListStatus.Failed)
        {
            return "Failed list";
        }
        else if (this.Status == ShoppingListStatus.New)
        {
            return "";
        }
        return string.Join("\n", this.Items == null ? new string[] { "" } : this.Items.ToArray()) + "\n\n" + Mathf.Ceil(this.GetTimeLeft());
    }
    private List<string> GenerateList(List<string> wordList)
    {
        var list = new List<string>();
        List<string> tempWordList = new List<string>(wordList);
        for (int i = 0; i < 5; i++)
        {
            if (tempWordList.Count == 0)
            {
                // All words have been picked, exit the loop
                break;
            }
            int randomIndex = Random.Range(0, tempWordList.Count);
            list.Add(tempWordList[randomIndex]);
            tempWordList.RemoveAt(randomIndex);
        }
        return list;
    }
}

public class RandomList : MonoBehaviour
{
    public int ListsCompleted = 0;
    private readonly float TimeLimitList = 30;

    private float? L1Time;
    private float? L2Time;
    private float? L3Time;

    public List<string> wordList;//all products in shop
    public GameObject beans;

    //private int score = 0;
    public ScoreManager scoreManager;
    public TimeManager timeManager;

    private TextMeshProUGUI listTextUI1;
    private TextMeshProUGUI listTextUI2;
    private TextMeshProUGUI listTextUI3;

    //private TextMeshProUGUI scoreTextUI;
    private TextMeshProUGUI messageTextUI;

    private ShoppingList List1;
    private ShoppingList List2;
    private ShoppingList List3;

    private string itemTag; //tag from item
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        timeManager = FindObjectOfType<TimeManager>();
        List1 = new ShoppingList(wordList);
        List2 = new ShoppingList(wordList);
        List3= new ShoppingList(wordList);
        List2.Status = ShoppingListStatus.New;
        List3.Status = ShoppingListStatus.New;

        listTextUI1 = GameObject.Find("List1").GetComponent<TextMeshProUGUI>();
        listTextUI2 = GameObject.Find("List2").GetComponent<TextMeshProUGUI>();
        listTextUI3 = GameObject.Find("List3").GetComponent<TextMeshProUGUI>();

        //scoreTextUI = GameObject.Find("Score (TMP)").GetComponent<TextMeshProUGUI>();
        messageTextUI = GameObject.Find("messageTextUI").GetComponent<TextMeshProUGUI>();

        StartCoroutine(CreateNewListAfterDelay(2, 12));
        StartCoroutine(CreateNewListAfterDelay(3, 24));
    }

    private void Update()
    {
        if (timeManager.GetTimeRemaining() > 0f)
        {
            //scoreTextUI.SetText("Score: " + score);

            listTextUI1.SetText(List1.FormattedList());
            listTextUI2.SetText(List2.FormattedList());
            listTextUI3.SetText(List3.FormattedList());

            if (List1.Status == ShoppingListStatus.Active && List1.IsTimeUp() == true)
            {
                List1.Status = ShoppingListStatus.Failed;
                scoreManager.DecreaseScore(10);
                StartCoroutine(CreateNewListAfterDelay(1, 5));
            }
            else if (List1.Status == ShoppingListStatus.Active && List1.Items.Count == 0)
            {
                scoreManager.ListCompleted();
                List1.Status= ShoppingListStatus.Completed;
                scoreManager.IncreaseScore(10);
                StartCoroutine(CreateNewListAfterDelay(1, 5));
            }

            if (List2.Status == ShoppingListStatus.Active && List2.IsTimeUp() == true)
            {
                List2.Status = ShoppingListStatus.Failed;
                scoreManager.DecreaseScore(10);
                StartCoroutine(CreateNewListAfterDelay(2, 5));
            }
            else if (List2.Status == ShoppingListStatus.Active && List2.Items.Count == 0)
            {
                scoreManager.ListCompleted();
                List2.Status = ShoppingListStatus.Completed;
                scoreManager.IncreaseScore(10);
                StartCoroutine(CreateNewListAfterDelay(2, 5));
            }

            if (List3.Status == ShoppingListStatus.Active && List3.IsTimeUp() == true)
            {
                List3.Status = ShoppingListStatus.Failed;
                scoreManager.DecreaseScore(10);
                StartCoroutine(CreateNewListAfterDelay(3, 5));
            }
            else if (List3.Status == ShoppingListStatus.Active && List3.Items.Count == 0)
            {
                scoreManager.ListCompleted();
                List3.Status = ShoppingListStatus.Completed;
                scoreManager.IncreaseScore(10);
                StartCoroutine(CreateNewListAfterDelay(3, 5));
            }
        }
        else
        {
            SceneManager.LoadScene(2);//end game
        }
    }

    public void ReceiveTag(string tag)
    {
        itemTag = tag.ToString();

        if (List1.Status == ShoppingListStatus.Active && List1.RemoveItem(tag))
        {
            scoreManager.IncreaseScore(1);
        }
        else if (List2.Status == ShoppingListStatus.Active && List2.RemoveItem(tag))
        {
            scoreManager.IncreaseScore(1);
        }
        else if (List3.Status == ShoppingListStatus.Active && List3.RemoveItem(tag))
        {
            scoreManager.IncreaseScore(1);
        }
        else
        {
            scoreManager.DecreaseScore(10);
            Instantiate(beans, GameObject.Find("InstantiatePunishLocation").GetComponent<Transform>().transform.position, GameObject.Find("InstantiatePunishLocation").GetComponent<Transform>().transform.rotation);
            print("punish complete");
        }
    }
    IEnumerator CreateNewListAfterDelay(int listID, int delay)
    {
        yield return new WaitForSeconds(delay);
        if (listID == 1)
        {
            List1 = new ShoppingList(wordList);
        }
        else if (listID == 2)
        {
            List2 = new ShoppingList(wordList);
        }
        else if (listID == 3)
        {
            List3 = new ShoppingList(wordList);
        }
    }
}
