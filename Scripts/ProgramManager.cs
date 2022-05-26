using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProgramManager : MonoBehaviour
{
    public TextMeshProUGUI programText;
    public Program program;
    private GameManager gameManager;

    public int factoryNum = 0;

    public void Awake()
    {
        programText.text = program.programName;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.OnMonthChanged += UseProgram;
    }

    public void UseProgram()
    {
        if (program.isUsed)
        {
            PlayerInfo.money -= program.cost;
            PlayerInfo.crimeRate -= program.crimeRateReduction;
            PlayerInfo.productionAmount += program.factoryProductionGain;
            PlayerInfo.support += program.popularityGain;
            PlayerInfo.foodProduction += program.foodProduction;

            if(program.name == "Factories")
            {
                if(factoryNum > 12) { factoryNum = 12; }

                PlayerInfo.money += Mathf.RoundToInt(program.factoryProductionGain * factoryNum);
                factoryNum += 1;
            }

            CapStats();
        }
    }

    public void CapStats()
    {
        if(PlayerInfo.crimeRate < 0) { PlayerInfo.crimeRate = 0; }
        if (PlayerInfo.support < 0) { PlayerInfo.support = 0; }
        if (PlayerInfo.support > 100) { PlayerInfo.support = 100; }
        if(PlayerInfo.foodProduction > 130) { PlayerInfo.foodProduction = 130; }

        PlayerInfo.crimeRate = Mathf.Round(PlayerInfo.crimeRate * 100f) / 100f;
        PlayerInfo.support = Mathf.Round(PlayerInfo.support * 100f) / 100f;

    }

    public void OnApplicationQuit()
    {
        program.isUsed = false;
    }


}
