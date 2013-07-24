using UnityEngine;
using System.Collections;
using System;

public class GameProperties_CG : GameProperties{

    public enum FRACTION { USFederation, EuropeCoaliation, AsianIndustryForce };
    public enum VEHICLES { Jeep, LightTank, MediumTank, HeavyTank, Helicopter, FighterJet, Bomber };

    public static int credits = 2000;

    public static float creditsPerTick = 20;
    public float creditsTempOverflow = 0;

    public static FRACTION fraction = FRACTION.EuropeCoaliation;

	// Use this for initialization
	private void Start () 
    {
	
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



}
