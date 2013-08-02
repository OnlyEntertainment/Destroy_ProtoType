using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class HeadQuarter_OLD20130802 : MonoBehaviour
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
    //public bool isUnitWorking = false;
    //public float unitWorkingSpeed = 10;
    //public int unitWorkingOn = 9;
    public float unitWorkingProcess = 0;

    public int unitWorkingLine = 0;

    private GameProperties_CT.WorkingUnit unitWorkingOn = new GameProperties_CT.WorkingUnit();

    public GameObject[] Prefabs = new GameObject[Enum.GetNames(typeof(GameProperties.VEHICLES)).Length];


    void Awake()
    {
        collectionPoint = gameObject.transform.position;
        initColor = gameObject.renderer.material.color;
    }

    public void DeleteFromProductionLine(GameProperties_CT.WorkingUnit unit)
    {
        if (unitWorkingLine > 0 &&
            unit.unitNumber == unitWorkingOn.unitNumber
            )
        {
            unitWorkingLine--;
            if (unitWorkingLine == 0) unitWorkingProcess = 0;
            GameProperties.credits += unitWorkingOn.cost;

        }
    }

    //public void ProduceUnit(int unitWorkingOn, float unitWorkingSpeed)
    public void ProduceUnit(GameProperties_CT.WorkingUnit unit)
    {
        if (this.unitWorkingLine == 0 &&
            HasEnoughMoney(unit))
        {

            this.unitWorkingOn = unit;
            this.unitWorkingLine++;
            this.unitWorkingProcess = 0;
            GameProperties.credits -= unitWorkingOn.cost;
            //this.unitWorkingOn = unitWorkingOn;
            //this.unitWorkingSpeed = unitWorkingSpeed;

        }
        //else if (unitWorkingLine > 0 && unitWorkingOn == this.unitWorkingOn && unitWorkingLine < unitWorkingLineMax)
        else if (unitWorkingLine > 0 &&
            unitWorkingOn.unitNumber == unit.unitNumber)
        {
            if (unitWorkingLine >= unitWorkingLineMax)
            { GameProperties_CT.AddMessage("Maximale Warteschlange erreicht."); }
            else if (HasEnoughMoney(unit))
            { 
                this.unitWorkingLine++;
                GameProperties.credits -= unitWorkingOn.cost;
            }
        }
    }

    private bool HasEnoughMoney(GameProperties_CT.WorkingUnit unit)
    {
        if (GameProperties.credits >= unit.cost)
        {
            return true;
        }
        else
        {
            GameProperties_CT.AddMessage("Du hast nicht genug Geld (" + unit.cost.ToString() + ")");
            return false;
        }
    }

    void Production()
    {
        if (unitWorkingLine > 0)
        {
            //unitWorkingProcess += unitWorkingSpeed * Time.deltaTime;
            unitWorkingProcess += unitWorkingOn.workingSpeed * Time.deltaTime;
            if (unitWorkingProcess >= 100.0f) //Einheit fertig gebaut
            {
                GameObject go = (GameObject)Instantiate(
                    unitWorkingOn.prefab,
                    gameObject.transform.position,
                    gameObject.transform.rotation);

                //GameObject go = (GameObject)Instantiate(
                //    GameProperties_CT.workingUnits[(GameProperties_CT.VEHICLES)unitWorkingOn].prefab, 
                //    gameObject.transform.position, 
                //    gameObject.transform.rotation);

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
                    
        ////Eigentliche Produktion der Einheiten
        //if (isUnitWorking)
        //{
        //    unitWorkingProcess += unitWorkingSpeed * Time.deltaTime;
        //    if (unitWorkingProcess >= 100)
        //    {
        //        if (Prefabs[unitWorkingOn - 1] != null)
        //        {
        //            GameObject go = (GameObject)Instantiate(Prefabs[unitWorkingOn - 1], gameObject.transform.position, gameObject.transform.rotation);
        //            go.transform.localScale = new Vector3(2, 2, 2);
        //            Unit goUnit = go.GetComponent<Unit>();
        //            goUnit.ownUnit_OnAction = "walk";
        //            goUnit.Unit_GoToPoint(collectionPoint);
        //        }

        //        isUnitWorking = false;
        //        unitWorkingOn = 9;
        //        unitWorkingProcess = 0;


        //    }
        //}   
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