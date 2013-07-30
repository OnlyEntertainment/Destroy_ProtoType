using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameProperties_CT: GameProperties{

    public enum FRACTION { USFederation, EuropeCoaliation, AsianIndustryForce };
    public enum VEHICLES { Jeep, LightTank, MediumTank, HeavyTank, Helicopter, FighterJet, Bomber };

    public struct WorkingUnit
    {
        public string name;
        public string toolTip;
        public int unitNumber;
        public float cost;
        public float workingSpeed;
    }

    public Dictionary<VEHICLES, WorkingUnit> workingUnits;

    public static int credits = 2000;

    public static float creditsPerTick = 20;
    public float creditsTempOverflow = 0;

    public static FRACTION fraction = FRACTION.EuropeCoaliation;

	// Use this for initialization
	private void Start () 
    {
	
	}

    private void Awake()
    {
        ConfigureUnitsWorking();
    }

	// Update is called once per frame
    private void Update()
    {
        creditsTempOverflow += creditsPerTick*Time.deltaTime;
        if (creditsTempOverflow >= creditsPerTick)
        {
            creditsTempOverflow -= creditsPerTick;
            credits += Convert.ToInt32(creditsPerTick);
        }
	}




    private void ConfigureUnitsWorking()
    {
        for (int i = 0; i <=  Enum.GetNames(typeof(VEHICLES)).Length; i++)
        {
            WorkingUnit unit = new WorkingUnit();
            switch (i)
            {
                case (int)VEHICLES.Jeep:
                    unit.unitNumber = i;
                    unit.name = "Jeep";
                    unit.toolTip = "Dies ist ein Jeep";
                    unit.cost = 1000.0f;
                    unit.workingSpeed = 10.0f;
                    break;
                case (int)VEHICLES.LightTank:
                    unit.unitNumber = i;
                    unit.name = "Leichter Panzer";
                    unit.toolTip = "Dies ist ein leichter Panzer";
                    unit.cost = 1000.0f;
                    unit.workingSpeed = 10.0f;
                    break;
                case (int)VEHICLES.MediumTank:
                    unit.unitNumber = i;
                    unit.name = "Mittlerer Panzer";
                    unit.toolTip = "Dies ist ein mittlerer Panzer";
                    unit.cost = 1000.0f;
                    unit.workingSpeed = 10.0f;
                    break;
                case (int)VEHICLES.HeavyTank:
                    unit.unitNumber = i;
                    unit.name = "Schwerer Panzer";
                    unit.toolTip = "Dies ist ein dicker fetter mächtiger Panzer";
                    unit.cost = 1000.0f;
                    unit.workingSpeed = 10.0f;
                    break;
                case (int)VEHICLES.Helicopter:
                    unit.unitNumber = i;
                    unit.name = "Helicopter";
                    unit.toolTip = "Dies ist ein Helischrauber oder Hubcopter";
                    unit.cost = 1000.0f;
                    unit.workingSpeed = 10.0f;
                    break;
                case (int)VEHICLES.FighterJet:
                    unit.unitNumber = i;
                    unit.name = "Jäger";
                    unit.toolTip = "Dies ist ein Jäger, der der fliegt nicht der am Boden";
                    unit.cost = 1000.0f;
                    unit.workingSpeed = 10.0f;
                    break;
                case (int)VEHICLES.Bomber:
                    unit.unitNumber = i;
                    unit.name = "Bomber";
                    unit.toolTip = "Dies ist ein Bomber und macht Boom";
                    unit.cost = 1000.0f;
                    unit.workingSpeed = 10.0f;
                    break;
                default:
                    unit.unitNumber = i;
                    unit.name = "-Unknown-";
                    unit.toolTip = "-Unknown-";
                    unit.cost = 999999.0f;
                    unit.workingSpeed = 0.1f;
                    break;
            }
            workingUnits.Add((VEHICLES)i, unit);
        }
        
      

       
    }

}
