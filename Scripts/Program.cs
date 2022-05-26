using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Program : ScriptableObject
{
    public string programName;

    public int cost; // per month

    public float factoryProductionGain;
    public float popularityGain;
    public int foodProduction;
    public float crimeRateReduction;

    public Sprite icon;

    public bool isUsed = false;
}
