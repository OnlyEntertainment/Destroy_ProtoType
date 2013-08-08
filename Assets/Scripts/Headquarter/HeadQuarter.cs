using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class HeadQuarter : MonoBehaviour
{

    public bool isActive = false;

    private Color initColor;
    private Color selectedColor = Color.cyan;
    private Color mouseOverColor = Color.blue;

    //Letzter Einheitensammelpunkt
    private RaycastHit hit_MouseCursor; //Informationsspeicher der Raycasts
    public Vector3 collectionPoint = Vector3.zero;

    //Einheitenbau
    public const int unitWorkingLineMax = 5;
    public float unitWorkingProcess = 0;
    public int unitWorkingLine = 0;

    private GameObject unitWorkingOn = null;
    private UnitClass unitScript = null;

    //public GameObject[] Prefabs = new GameObject[Enum.GetNames(typeof(ENUMS.VEHICLES)).Length];


    void Awake()
    {
        collectionPoint = gameObject.transform.position;
        initColor = gameObject.renderer.material.color;
    }

    public void DeleteFromProductionLine(GameObject unitObject)
    {
        UnitClass tmpUnitScript = unitObject.GetComponent<UnitClass>();


        if (unitWorkingLine > 0 &&
            tmpUnitScript.type == unitScript.type
            )
        {
            unitWorkingLine--;
            if (unitWorkingLine == 0) unitWorkingProcess = 0;
            GameProperties.credits += unitScript.cost[GameProperties_CT.levelOfAllUnits[unitScript.type][ENUMS.PROPERTIES.Cost]];

        }
    }

    //public void ProduceUnit(int unitWorkingOn, float unitWorkingSpeed)
    public void ProduceUnit(GameObject unitObject)
    {
        UnitClass tmpUnitScript = unitObject.GetComponent<UnitClass>();

        if (this.unitWorkingLine == 0 &&
            HasEnoughMoney(tmpUnitScript))
        {

            this.unitWorkingOn = unitObject;
            this.unitScript = tmpUnitScript;
            this.unitWorkingLine++;
            this.unitWorkingProcess = 0;
            GameProperties.credits -= GameProperties_CT.levelOfAllUnits[unitScript.type][ENUMS.PROPERTIES.Cost];


        }
        else if (unitWorkingLine > 0 &&
            unitWorkingOn == tmpUnitScript)
        {
            if (unitWorkingLine >= unitWorkingLineMax)
            { GameProperties_CT.AddMessage("Maximale Warteschlange erreicht."); }
            else if (HasEnoughMoney(tmpUnitScript))
            { 
                this.unitWorkingLine++;
                GameProperties.credits -= unitScript.cost[GetLevel(ENUMS.PROPERTIES.Cost)];
            }
        }
    }

    private bool HasEnoughMoney(UnitClass tmpUnitScript)
    {
        int tmpCost = tmpUnitScript.cost[GameProperties_CT.levelOfAllUnits[tmpUnitScript.type][ENUMS.PROPERTIES.Cost]];

        if (GameProperties.credits >= tmpCost)
        {
            return true;
        }
        else
        {
            GameProperties_CT.AddMessage("Du hast nicht genug Geld (" + tmpCost.ToString() + ")");
            return false;
        }
    }

    int GetLevel(ENUMS.PROPERTIES propertie)
    {
        return GameProperties_CT.levelOfAllUnits[unitScript.type][propertie];
    }

    void Production()
    {
        if (unitWorkingLine > 0)
        {
            unitWorkingProcess += unitScript.speed[GetLevel(ENUMS.PROPERTIES.Speed)] * Time.deltaTime;
            if (unitWorkingProcess >= 100.0f) //Einheit fertig gebaut
            {
                GameObject go = (GameObject)Instantiate(
                    unitWorkingOn,
                    gameObject.transform.position,
                    gameObject.transform.rotation);

                go.transform.localScale = new Vector3(2, 2, 2);
                Unit goUnit = go.GetComponent<Unit>();
                goUnit.ownUnit_OnAction = "walk";
                goUnit.Unit_GoToPoint(collectionPoint);
                unitWorkingLine--;
                unitWorkingProcess = 0;
            }
        }
    }

    void Update()
    {

        //Farbe anpassen wenn HQ Aktiv
        if (isActive) { gameObject.renderer.material.color = selectedColor; }

        //Wenn Aktiv und Rechtsklick gemacht wird, dann wird neuer Sammeölpunkt gesetzt.
        if (isActive && Input.GetMouseButtonDown(1) && Physics.Raycast(Camera.mainCamera.ScreenPointToRay(Input.mousePosition), out hit_MouseCursor, Mathf.Infinity, 512/*9: Weg collider*/))
        {
            collectionPoint = hit_MouseCursor.point;
        }

        Production();
        
    }

    private void OnMouseEnter()
    {
        gameObject.renderer.material.color = mouseOverColor;
    }

    private void OnMouseExit()
    {
        gameObject.renderer.material.color = initColor;
    }

    private void OnMouseDown()
    {
        if (!isActive)
        {
            Global_Terrain.DeselectAll();
            isActive = true;
        }
    }

    public void SetInActive()
    {
        if (isActive)
        {
            isActive = false;
            gameObject.renderer.material.color = initColor;
        }
    }

}