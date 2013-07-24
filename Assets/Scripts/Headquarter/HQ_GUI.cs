using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

public class HQ_GUI : MonoBehaviour
{
    private bool isActive = false;
    private Color initColor;

    //Allgemein SideBar Properties
    public float sideBarX = 0f;
    private float sideBarY = 0f;
    private float sideBarWidth = 230f;
    private float sideBarHeight = 550f;

    private string labelToolTip = "";
    private int buttonSizeHeight = 50;
    private int buttonSizeWidth = 100;

    //Sliding
    public bool isSlidingIn = false;
    public bool isSlidingOut = false;

    private float sideBarStartX;
    private float sideBarEndX;

    public float slidingSpeed = 2.0f;

    //Einheitenbau
    public bool isUnitWorking = false;
    public int unitWorkingOn = 9;
    public float unitWorkingProcess = 0;
    public float unitWorkingStep = 10;


    ////Forschung 
    //public bool isResearchWorking = false;
    //public int researchWorkingOn = 9;
    //public float researchWorkingProcess = 0;
    //public float researchWorkingStep = 10;

    //ForschungsIcons
    public Texture2D iconCreditsPerTick;
    public GameObject[] Prefabs =  new GameObject[Enum.GetNames(typeof(GameProperties.VEHICLES)).Length];
    //public Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject> { Enum.GetNames(typeof(GameProperties.VEHICLES)), "" };

    //Letzter Einheitensammelpunkt
    private RaycastHit hit_MouseCursor; //Informationsspeicher der Raycasts
    public  Vector3 collectionPoint = Vector3.zero;

    //Temporäreres bzw. Stuff
    Texture2D tmpIcon = null;

    //STYLES--------------------------------------------------------------------------------------------------------------------------------
    private GUIStyle buttonStyle;
    //private Color initButtonColor;

    //STYLES END --------------------------------------------------------------------------------------------------------------------------------

    // Use this for initialization
    private void Start()
    {
        collectionPoint = gameObject.transform.position;

        initColor = gameObject.renderer.material.color;
        sideBarStartX = Screen.width;
        sideBarEndX = Screen.width - sideBarWidth;
        sideBarX = sideBarStartX;
    }

    // Update is called once per frame
    private void Update()
    {
        sideBarStartX = Screen.width; //DEBUG
        sideBarEndX = Screen.width - sideBarWidth;//DEBUG

        Sliding();

        if (isUnitWorking)
        {
            unitWorkingProcess += unitWorkingStep * Time.deltaTime;
            if (unitWorkingProcess >= 100)
            {
                if (Prefabs[unitWorkingOn-1] != null)
                {
                GameObject go = (GameObject)Instantiate(Prefabs[unitWorkingOn - 1], gameObject.transform.position, gameObject.transform.rotation);
                go.transform.localScale = new Vector3(2, 2, 2);
                Unit goUnit = go.GetComponent<Unit>();
                goUnit.ownUnit_OnAction = "walk";
                goUnit.Unit_GoToPoint(collectionPoint);
                    
                //goUnit.ownUnit_TargetPoint = collectionPoint;
                //goUnit.ownUnit_OnAction = "walk";
                //goUnit.ownUnit_OnAction = "walk";
                //goUnit.ownUnit_OnAction = "walk";
                //goUnit.ownUnit_OnAction = "walk";
                //goUnit.ownUnit_OnAction = "walk";
                }
                
                    //foreach (GameObject tempCurrentSelectedUnit in currentSelectedUnits)
                    //{
                    //tempCurrentSelectedUnit.GetComponent<Unit>().Unit_GoToPoint(hit_MouseCurser .point);
                    //}
                isUnitWorking = false;
                unitWorkingOn = 9;
                unitWorkingProcess = 0;
                

            }
        }   
        if (isActive && Input.GetMouseButtonDown(1) && Physics.Raycast(Camera.mainCamera.ScreenPointToRay(Input.mousePosition), out hit_MouseCursor, Mathf.Infinity, 512/*9: Weg collider*/))
        {
            collectionPoint = hit_MouseCursor.point;            
        }
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(sideBarX, sideBarY, sideBarWidth, sideBarHeight));

        GUI.Box(new Rect(0, 0, sideBarWidth, sideBarHeight), "");

        GUIUnitBuilding();

        //GUIResearchBuilding();

        //Tooltip Box
        Rect cellRect = new Rect(10, 10 + (10 + buttonSizeHeight), buttonSizeWidth + 10 + buttonSizeWidth, buttonSizeHeight);
        GUI.Box(cellRect, "");
        GUI.Label(cellRect, GUI.tooltip);

        GUILayout.EndArea();
    }


    private void OnMouseEnter()
    {
        gameObject.renderer.material.color = Color.cyan;
    }

    private void OnMouseExit()
    {
        gameObject.renderer.material.color = initColor;
    }

    private void OnMouseDown()
    {
        if (!isActive)
        {
            SlideIN();
            Global_Terrain.DeselectAll();
            isActive = true;
        }
    }


    private void GUIUnitBuilding()
    {

        GetStyles();


        int unitCount = Enum.GetNames(typeof(GameProperties.VEHICLES)).Length;
        int unit = 1;
        int cols = 2;

        int[] rows = new int[2] { 4, 3 };

        for (int col = 1; col <= cols; col++)
        {
            for (int row = 1; row <= rows[col - 1]; row++)
            {
                Rect cellRect = new Rect(10 + (10 + buttonSizeWidth) * (col - 1), 10 + (10 + buttonSizeHeight) * (row + 1), buttonSizeWidth, buttonSizeHeight);

                GUIContent cont = GetGUIContent(unit);

                Color color = buttonStyle.normal.textColor;
                if (unit == unitWorkingOn)
                {
                    float workingProcess = 0;
                    workingProcess = unitWorkingProcess;
                    GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, workingProcess / 100f);
                    cont.text += "\n(" + Convert.ToInt32(workingProcess).ToString() + "%)";
                }
                else
                {
                    GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, 1);
                }

                if (tmpIcon != null) { GUI.DrawTexture(cellRect, tmpIcon); }

                if (isUnitWorking)
                {
                    GUI.Label(cellRect, cont, buttonStyle);
                }
                else
                {
                    if (GUI.Button(cellRect, cont, buttonStyle))
                    {
                        isUnitWorking = true;
                        unitWorkingOn = unit;
                        unitWorkingProcess = 0;
                    }
                }
                unit++;
            }
        }
    }


    private GUIContent GetGUIContent(int unit)
    {
        GUIContent content = new GUIContent();

        switch (unit)
        {
            case 1:
                content.text = "Jeep";
                content.tooltip = "Dies ist ein Jeep";
                tmpIcon = null;
                break;
            case 2:
                content.text = "L. Panzer";
                content.tooltip = "Dies ist ein Leichter Panzer";
                tmpIcon = null;
                break;
            case 3:
                content.text = "M. Panzer";
                content.tooltip = "Dies ist ein Mittlerer Panzer";
                tmpIcon = iconCreditsPerTick;
                break;
            case 4:
                content.text = "S. Panzer";
                content.tooltip = "Dies ist ein Schwerer Panzer";
                tmpIcon = null;
                break;
            case 5:
                content.text = "Helikopter";
                content.tooltip = "Dies ist ein Helikopter";
                tmpIcon = null;
                break;
            case 6:
                content.text = "Jäger";
                content.tooltip = "Dies ist ein Jäger";
                tmpIcon = null;
                break;
            case 7:
                content.text = "Bomber";
                content.tooltip = "Dies ist ein Bomber";
                tmpIcon = null;
                break;
        }
        return content;
    }


    public void SetInActive()
    {
        if (isActive)
        {
            isSlidingOut = true;
            isSlidingIn = false;
            isActive = false;
            gameObject.renderer.material.color = initColor;
        }
    }

    private void SlideIN()
    {
        isSlidingIn = true;
        isSlidingOut = false;

        //sideBarX = beginnX;
    }

    private void Sliding()
    {
        if (isActive)
        {
            gameObject.renderer.material.color = Color.cyan;
        }

        if (isSlidingIn)
        {
            if (sideBarX >= sideBarEndX) sideBarX -= slidingSpeed * Time.deltaTime;
            else
            {
                isSlidingIn = false;
                sideBarX = sideBarEndX;
            }
        }


        if (isSlidingOut)
        {
            if (sideBarX <= sideBarStartX) sideBarX += slidingSpeed * Time.deltaTime;
            else
            {
                isSlidingOut = false;
                sideBarX = sideBarStartX;
            }
        }
    }

    private void GetStyles()
    {
        //ButtonStyle
        buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.normal.textColor = Color.white;
        buttonStyle.fontStyle = FontStyle.Bold;
        buttonStyle.fontSize = 11;
        buttonStyle.normal.background = buttonStyle.hover.background = buttonStyle.active.background = null;
        //initButtonColor = buttonStyle.normal.textColor;
    }


}
