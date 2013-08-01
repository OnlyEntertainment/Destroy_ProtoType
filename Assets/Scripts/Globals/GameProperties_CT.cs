using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameProperties_CT : GameProperties
{

    private static List<string> messages = new List<string>();

    public enum FRACTION { USFederation, EuropeCoaliation, AsianIndustryForce };
    public enum VEHICLES { Jeep, LightTank, MediumTank, HeavyTank, Helicopter, FighterJet, Bomber };

    public static GameObject g;
    public Dictionary<VEHICLES, GameObject> unitPrefabs = new Dictionary<VEHICLES, GameObject>() 
    {
        {VEHICLES.Jeep,null},
        {VEHICLES.LightTank,null},
        {VEHICLES.MediumTank,null},
        {VEHICLES.HeavyTank,null},
        {VEHICLES.Helicopter,null},
        {VEHICLES.FighterJet,null},
        {VEHICLES.Bomber,null}
    };


    public struct WorkingUnit
    {
        public string name;
        public string toolTip;
        public int unitNumber;
        public int cost;
        public float workingSpeed;
        public GameObject prefab;
    }

    public static Dictionary<VEHICLES, WorkingUnit> workingUnits;

    public static int credits = 2000;

    public static float creditsPerTick = 20;
    public float creditsTempOverflow = 0;

    public static FRACTION fraction = FRACTION.EuropeCoaliation;

    // Use this for initialization
    private void Start()
    {

    }

    private void Awake()
    {
        ConfigureUnitsWorking();
    }

    // Update is called once per frame
    private void Update()
    {
        creditsTempOverflow += creditsPerTick * Time.deltaTime;
        if (creditsTempOverflow >= creditsPerTick)
        {
            creditsTempOverflow -= creditsPerTick;
            credits += Convert.ToInt32(creditsPerTick);
        }
    }

    public static void AddMessage(string text)
    {
        messages.Add(text);
    }
    


    private void ConfigureUnitsWorking()
    {
        for (int i = 0; i <= Enum.GetNames(typeof(VEHICLES)).Length; i++)
        {
            WorkingUnit unit = new WorkingUnit();
            switch (i)
            {
                case (int)VEHICLES.Jeep:
                    unit.name = "Jeep";
                    unit.toolTip = "Dies ist ein Jeep";
                    unit.cost = 1000;
                    unit.workingSpeed = 10.0f;
                    break;
                case (int)VEHICLES.LightTank:
                    //unit.unitNumber = i;
                    unit.name = "Leichter Panzer";
                    unit.toolTip = "Dies ist ein leichter Panzer";
                    unit.cost = 1000;
                    unit.workingSpeed = 10.0f;
                    break;
                case (int)VEHICLES.MediumTank:
                    //unit.unitNumber = i;
                    unit.name = "Mittlerer Panzer";
                    unit.toolTip = "Dies ist ein mittlerer Panzer";
                    unit.cost = 1000;
                    unit.workingSpeed = 10.0f;
                    break;
                case (int)VEHICLES.HeavyTank:
                    //unit.unitNumber = i;
                    unit.name = "Schwerer Panzer";
                    unit.toolTip = "Dies ist ein dicker fetter mächtiger Panzer";
                    unit.cost = 1000;
                    unit.workingSpeed = 10.0f;
                    break;
                case (int)VEHICLES.Helicopter:
                    //unit.unitNumber = i;
                    unit.name = "Helicopter";
                    unit.toolTip = "Dies ist ein Helischrauber oder Hubcopter";
                    unit.cost = 1000;
                    unit.workingSpeed = 10.0f;
                    break;
                case (int)VEHICLES.FighterJet:
                    //unit.unitNumber = i;
                    unit.name = "Jäger";
                    unit.toolTip = "Dies ist ein Jäger, der der fliegt nicht der am Boden";
                    unit.cost = 1000;
                    unit.workingSpeed = 10.0f;
                    break;
                case (int)VEHICLES.Bomber:
                    //unit.unitNumber = i;
                    unit.name = "Bomber";
                    unit.toolTip = "Dies ist ein Bomber und macht Boom";
                    unit.cost = 1000;
                    unit.workingSpeed = 10.0f;
                    break;
                default:
                    //unit.unitNumber = i;
                    unit.name = "-Unknown-";
                    unit.toolTip = "-Unknown-";
                    unit.cost = 999999999;
                    unit.workingSpeed = 0.1f;
                    break;                    
            }
            unit.unitNumber = i;
            unit.prefab = unitPrefabs[(VEHICLES)i];

            workingUnits.Add((VEHICLES)i, unit);
        }




    }

}
