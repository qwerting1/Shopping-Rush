using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public enum ShoppingListStatus
{
    New,
    Respawning,
    Active,
    Completed,
    JustFailed,
    Failed,
    Old
}

public class ShoppingList
{
    public ShoppingListStatus Status = ShoppingListStatus.New;
    public List<string> Items = new List<string>();
    public float StartTime;
    public float TimeLimit;
    public float? TimeRemaining;
    public Color textColor;
    public float respawnStartTime;
    public float respawnTime;
    private List<string> wordList;
    private ScoreManager scoreManager;

    public ShoppingList(List<string> wordList, ScoreManager scoreManager)
    {
        this.Status = ShoppingListStatus.New;
        this.wordList = wordList;
        this.textColor = Color.white;
        this.scoreManager = scoreManager;
    }

    public void Update()
    {
        if (this.Status == ShoppingListStatus.New)
        {
            this.Status = ShoppingListStatus.Respawning;
            this.respawnStartTime = Time.time;
            this.respawnTime = Random.Range(5, 15);
        }

        if (this.Status != ShoppingListStatus.Active)
        {
            this.textColor = Color.white;
            if (Time.time > this.respawnStartTime + this.respawnTime)
            {
                this.StartList();
            }
            return;
        }
        // Active lists only below here
        var isTimeUp = Time.time > this.StartTime + this.TimeLimit;
        if (isTimeUp)
        {
            if (this.Items.Count > 0)
            {
                this.scoreManager.DecreaseScore(10);
            }
            this.Status = ShoppingListStatus.JustFailed;
            this.respawnStartTime = Time.time;
            this.respawnTime = Random.Range(5, 20);
            this.TimeRemaining = null;
        }
        else
        {
            if (this.Items.Count == 0)
            {
                this.scoreManager.IncreaseScore(10);
                this.respawnStartTime = Time.time;
                this.respawnTime = Random.Range(5, 15);
                this.Status = ShoppingListStatus.Completed;
                this.TimeRemaining = null;
                this.scoreManager.ListCompleted();
            }
            else
            {
                this.TimeRemaining = (this.StartTime + this.TimeLimit) - Time.time;
                if (this.TimeRemaining < 5)
                {
                    this.textColor = Color.red;
                }
                else if (this.TimeRemaining < 10)
                {
                    this.textColor = Color.yellow;
                }
                else {
                    this.textColor = Color.green;
                }
            }
        }
    }

    public bool RemoveItem(string item)
    {
        return this.Items.Remove(item);
    }

    public string FormattedList()
    {
        if (this.Status == ShoppingListStatus.Completed)
        {
            return "Completed list";
        }
        else if (this.Status == ShoppingListStatus.Failed)
        {
            return "Failed list";
        }
        else if (this.Status == ShoppingListStatus.New)
        {
            return "";
        }
        if (this.Status == ShoppingListStatus.Active)
        {
            return string.Join("\n", this.Items == null ? new string[] { "" } : this.Items.ToArray()) + "\n\n" + Mathf.Ceil(this.TimeRemaining ?? 0);
        }
        return "";

    }
    public void StartList()
    {
        this.Status = ShoppingListStatus.Active;
        this.StartTime = Time.time;
        var list = new List<string>();
        List<string> tempWordList = new List<string>(this.wordList);
        for (int i = 0; i < Random.Range(3, 5); i++)
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
        this.TimeLimit = list.Count * Random.Range(8,14);
        this.Items = list;
    }
}

public class RandomList : MonoBehaviour
{
    public int ListsCompleted = 0;

    public List<string> wordList;//all products in shop
    public GameObject beans;
    private List<ShoppingList> activeListsSortedByTime;

    public AudioSource Correct;
    public AudioSource Fail;
    public AudioSource Wrong;
    public AudioSource TimeS;

    public GameObject List1Background;
    public GameObject List2Background;
    public GameObject List3Background;

    private TimeManager timeManager;//used to be public fyi
    private ScoreManager scoreManager;

    private TextMeshProUGUI listTextUI1;
    private TextMeshProUGUI listTextUI2;
    private TextMeshProUGUI listTextUI3;

    private ShoppingList ShoppingList1;
    private ShoppingList ShoppingList2;
    private ShoppingList ShoppingList3;

    private bool isAllowedToQuit = true;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        timeManager = FindObjectOfType<TimeManager>();
        ShoppingList1 = new ShoppingList(wordList, scoreManager);
        ShoppingList1.StartList();
        ShoppingList2 = new ShoppingList(wordList, scoreManager);
        ShoppingList3 = new ShoppingList(wordList, scoreManager);

        listTextUI1 = GameObject.Find("List1").GetComponent<TextMeshProUGUI>();
        listTextUI2 = GameObject.Find("List2").GetComponent<TextMeshProUGUI>();
        listTextUI3 = GameObject.Find("List3").GetComponent<TextMeshProUGUI>();

    }

    private void Update()
    {
        if(ShoppingList1.Status == ShoppingListStatus.Active)
        {
            List1Background.SetActive(true);
        }
        if (ShoppingList2.Status == ShoppingListStatus.Active)
        {
            List2Background.SetActive(true);
        }
        if (ShoppingList3.Status == ShoppingListStatus.Active)
        {
            List3Background.SetActive(true);
        }

        if (ShoppingList1.Status == ShoppingListStatus.JustFailed)
        {
            ShoppingList1.Status = ShoppingListStatus.Failed;
            //print("failed");
            Wrong.Play();
        }
        if (ShoppingList2.Status == ShoppingListStatus.JustFailed)
        {
            ShoppingList2.Status = ShoppingListStatus.Failed;
            //print("failed");
            Wrong.Play();
        }
        if (ShoppingList3.Status == ShoppingListStatus.JustFailed)
        {
            ShoppingList3.Status = ShoppingListStatus.Failed;
            //print("failed");
            Wrong.Play();
        }


        ShoppingList1.Update();
        ShoppingList2.Update();
        ShoppingList3.Update();

        activeListsSortedByTime = new List<ShoppingList>();
        if (ShoppingList1.TimeRemaining != null)
        {
            activeListsSortedByTime.Add(ShoppingList1);
        }
        if (ShoppingList2.TimeRemaining != null)
        {
            activeListsSortedByTime.Add(ShoppingList2);
        }
        if (ShoppingList3.TimeRemaining != null)
        {
            activeListsSortedByTime.Add(ShoppingList3);
        }
        if (activeListsSortedByTime.Count > 0)
        {
            activeListsSortedByTime.Sort((x, y) => x.TimeRemaining > y.TimeRemaining ? 1 : 0);
        }

        if (timeManager.GetTimeRemaining() <= 0f)
        {
            if(isAllowedToQuit)
            {
                isAllowedToQuit = false;
                timeManager.AddTimeRemaining((float)activeListsSortedByTime[0].TimeRemaining + 1f);
                timeManager.SetTimerTextColor(Color.red);
            }
            if (timeManager.GetTimeRemaining() <= 0f)
            {
                SceneManager.LoadScene(2);//end game
                return;
            }
    }

        listTextUI1.color = ShoppingList1.textColor;
        listTextUI2.color = ShoppingList2.textColor;
        listTextUI3.color = ShoppingList3.textColor;

        if (Mathf.Ceil(ShoppingList1.TimeRemaining ?? 0) == 6)
        {
            //print("time warning");
            TimeS.Play();
        }
        if (Mathf.Ceil(ShoppingList2.TimeRemaining ?? 0) == 6)
        {
            //print("time warning");
            TimeS.Play();
        }
        if (Mathf.Ceil(ShoppingList3.TimeRemaining ?? 0) == 6)
        {
            //print("time warning");
            TimeS.Play();
        }

        if (Mathf.Ceil(ShoppingList1.TimeRemaining ?? 0) == 11)
        {
            //print("time warning");
            TimeS.Play();
        }
        if (Mathf.Ceil(ShoppingList2.TimeRemaining ?? 0) == 11)
        {
            //print("time warning");
            TimeS.Play();
        }
        if (Mathf.Ceil(ShoppingList3.TimeRemaining ?? 0) == 11)
        {
            //print("time warning");
            TimeS.Play();
        }

        listTextUI1.SetText(ShoppingList1.FormattedList());
        listTextUI2.SetText(ShoppingList2.FormattedList());
        listTextUI3.SetText(ShoppingList3.FormattedList());


    }

    public void ReceiveTag(string tag)
    {
        var foundItem = false;
        foreach (var list in activeListsSortedByTime)
        {
            if (foundItem == false && list.Items.Contains(tag))
            {
                Correct.Play();
                scoreManager.IncreaseScore(1);
                list.RemoveItem(tag);
                foundItem = true;
            }
        }
        if(foundItem == false)
        {
            Fail.Play();
            scoreManager.DecreaseScore(10);
            //Instantiate(beans, GameObject.Find("InstantiatePunishLocation").GetComponent<Transform>().transform.position, GameObject.Find("InstantiatePunishLocation").GetComponent<Transform>().transform.rotation);
            //print("punish complete");
        }

    }
}
