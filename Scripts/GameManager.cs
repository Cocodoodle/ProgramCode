using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI dateText;
    public float timeTillNextMonth;
    public TextMeshProUGUI moneyText;

    private float year = 1;

    float timer;

    public List<string> months = new List<string>();
    private int monthIndex = 0;
    private string currentMonth;

    public delegate void MonthChange();
    public event MonthChange OnMonthChanged;

    public MessageManager messageManager;

    public TextMeshProUGUI populatityText;
    public TextMeshProUGUI foodText;
    public TextMeshProUGUI crimeText;

    private int goodCounter = 0;
    private bool startGoodCounter = false;

    private int supportCounter = 0;
    private bool startSupportCounter = false;

    public GameObject winBox;
    public TextMeshProUGUI winTitle;
    public TextMeshProUGUI winContant;

    public Program supportProgram;
    public ProgramManager factoryManager;

    private void Start()
    {
        messageManager.CreateMail("Tutorial", "Welcome to the game");
        currentMonth = months[0];
        dateText.text = "Y:  " + year + "     M:  " + currentMonth; 
    }

    void Update()
    {
        moneyText.text = "Money:  " + PlayerInfo.money;

        SetStats();
        CheckIfBackrupt();
        WinCondition();

        ChangeDate();
    }

    public void ChangeDate()
    {
        timer += Time.deltaTime;

        if(timer > timeTillNextMonth)
        {
            if (monthIndex > 10)
            {
                year += 1;
                monthIndex = 0;
            }
            else
            {
                monthIndex += 1;
            }

            OnMonthChanged();
            PlayerInfo.money += PlayerInfo.income;

            PlayerInfo.crimeRate += 0.3f;
            PlayerInfo.support -= 0.3f;
            PlayerInfo.foodProduction -= 1;

            HandleCircumstances();

            currentMonth = months[monthIndex];
            dateText.text = "Y:  " + year + "     M:  " + currentMonth;

            timer = 0;

            if (startGoodCounter)
            {
                goodCounter += 1;
            }

            if (startSupportCounter)
            {
                supportCounter += 1;
            }

            HappyDays();
            NoBlackmail();
        }
    }

    public void SetStats()
    {
        if (PlayerInfo.support > 50)
        {
            populatityText.color = Color.green;
        }
        else if(PlayerInfo.support <= 50)
        {
            populatityText.color = Color.red;
        }

        if (PlayerInfo.foodProduction >= 100)
        {
            foodText.color = Color.green;
        }
        else if(PlayerInfo.foodProduction < 100)
        {
            foodText.color = Color.red;
        }

        if (PlayerInfo.crimeRate < 15)
        {
           crimeText.color = Color.green;
        }
        else if(PlayerInfo.crimeRate >= 15)
        {
            crimeText.color = Color.red;
        }

        populatityText.text = PlayerInfo.support.ToString() + "%";
        foodText.text = PlayerInfo.foodProduction.ToString() + "%";
        crimeText.text = PlayerInfo.crimeRate.ToString() + "%";
    }

    public void HandleCircumstances()
    {
        CrimeOverload();
        StarvingPeople();
        LackOfGoods();
        Winter();
        Spring();
        Coup();
        Riots();
        Blackmail();
        FactoryFailure();
    }

    public void CrimeOverload()
    {
        int n = Random.Range(0, 7);

        if(n == 2 && PlayerInfo.crimeRate >= 15)
        {
            messageManager.CreateMail("Robbery", "Due to the high crime rate, a bank robbery cost your city 2000 dollars");
            PlayerInfo.money -= 2000;
        }

    }

    public void StarvingPeople()
    {
        int n = Random.Range(0, 7);

        if (n == 2 && PlayerInfo.foodProduction < 100)
        {
            messageManager.CreateMail("Food Shortage", "Due to the insufficient amount of food, people hate you. -5% popularity");
            PlayerInfo.support -= 5f;
        }
    }

    public void LackOfGoods()
    {
        int n = Random.Range(0, 10);

        if (n == 2 && goodCounter == 0)
        {
            messageManager.CreateMail("Lack of Goods", "Due to the lack of goods expoted, you income drops by 425 dollars");
            PlayerInfo.income -= 425;
            startGoodCounter = true;
        }
    }

    public void HappyDays()
    {
        if (goodCounter > 6)
        {
            messageManager.CreateMail("Happy Days", "You are no longer have low production");
            PlayerInfo.income += 425;
            goodCounter = 0;
            startGoodCounter = false;
        }
    }

    public void Winter()
    {
        if(months[monthIndex] == "Oct.")
        {
            messageManager.CreateMail("Winter", "Winter has arrived! Food production has gone down by 10 percent");
            PlayerInfo.foodProduction -= 15;
        }
    }

    public void Spring()
    {
        if (months[monthIndex] == "Mar." && year != 1)
        {
            messageManager.CreateMail("Spring", "Spring has arrived! Food production has gone back up by 10 percent");
            PlayerInfo.foodProduction += 15;
        }
    }

    public void Coup()
    {
        int n = Random.Range(0, 8);

        if(PlayerInfo.support > 60f && n == 2)
        {
            if(PlayerInfo.crimeRate < 10)
            {
                messageManager.CreateMail("Coup", "Due to you increasing popularity, your oppnents staged a coup. Luckly, law enforcment stopped it before too much damage was done. -750 dollars");
                PlayerInfo.money -= 750;
            }
            else
            {
                messageManager.CreateMail("Coup", "Due to you increasing popularity, your oppnents staged a coup. Unfortunately, law enforcment couldn't stop it before too much damage was done. - 2250 dollars");
                PlayerInfo.money -= 2250;
            }
        }
    }

    public void Riots()
    {
        int n = Random.Range(0, 13);

        if (n == 2)
        {
            if (PlayerInfo.crimeRate < 10)
            {
                messageManager.CreateMail("Riots", "Due to you increasing popularity, your oppnents started riots. Luckly, law enforcment stopped it before too much damage was done. -750 dollars");
                PlayerInfo.money -= 750;
            }
            else
            {
                messageManager.CreateMail("Riots", "Due to you increasing popularity, your oppnents started riots. Unfortunately, law enforcment couldn't stop it before too much damage was done. - 2250 dollars");
                PlayerInfo.money -= 2250;
                PlayerInfo.foodProduction -= 7;
            }
        }
    }

    public void CheckIfBackrupt()
    {
        if(PlayerInfo.money <= 0)
        {
            winBox.SetActive(true);
            winTitle.text = "You Lose!";
            winContant.text = "The city went bankrupt and you were kick out of office!";

            winTitle.color = Color.red;
            winContant.color = Color.red;
        }
    }

    public void WinCondition()
    {
        if (year >= 4)
        {
            if (PlayerInfo.support > 50f)
            {
                winBox.SetActive(true);
                winTitle.text = "You Win!";
                winContant.text = "Due to you massive support, you won the next election!";
            }
            else if (PlayerInfo.support <= 50f)
            {
                winBox.SetActive(true);
                winTitle.text = "You Lose!";
                winContant.text = "You were a bad mayor. You got canceled!";

                winTitle.color = Color.red;
                winContant.color = Color.red;
            }

        }
    }

    public void Blackmail()
    {
        int n = Random.Range(0, 13);

        if(n == 2 && goodCounter == 0 && supportProgram.isUsed)
        {
            messageManager.CreateMail("Blackmail", "You opponents leaked secrets about you which caused the public to hate you.  -0.5 support gain");
            supportProgram.popularityGain -= 0.5f;
            startSupportCounter = true;
        }
    }

    public void NoBlackmail()
    {
        if (supportCounter > 4)
        {
            messageManager.CreateMail("No Blackmail", "The people have forgotten about the secrets. +0.5 support gain");
            supportProgram.popularityGain += 0.5f;
            startSupportCounter = false;
            supportCounter = 0;
        }
    }

    public void FactoryFailure()
    {
        int n = Random.Range(0, 13);

        if(factoryManager.factoryNum > 4 && n == 2)
        {
            messageManager.CreateMail("Factory Failure", "The factories are broken!");
            factoryManager.factoryNum = 4;
        }

    }

}
