using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System;

public class GlobalGUI_CT : GlobalGUI
{
#region Variables

    #region HQ

    public GameObject HeadQuarter;
    private HeadQuarter hqScript;
    //Sliding
    public bool isSlidingIn = false;
    public bool isSlidingOut = false;

    private float sideBarStartX;
    private float sideBarEndX;

    //Allgemein SideBar Properties
    float sideBarX = 0f; 
    float sideBarY = 0f;

    float sideBarWidth = 230f;
    float sideBarHeight = 550f;

    string labelToolTip = "";
    int buttonSizeHeight = 50;
    int buttonSizeWidth = 100;

    public float slidingSpeed = 2.0f;

    //Temporäreres bzw. Stuff
    Texture2D tmpIcon = null;

    public Texture2D iconCreditsPerTick;
  
    //STYLES--------------------------------------------------------------------------------------------------------------------------------
    private GUIStyle buttonStyle;
    //private Color initButtonColor;

    //STYLES END --------------------------------------------------------------------------------------------------------------------------------


    #endregion HQ

    #region menu (Top)
    public Texture2D menuTopBarBackground;
    #endregion menu (Top)

#endregion Variables

    void Start()
    {
        //HQ
        hqScript = HeadQuarter.GetComponent<HeadQuarter>();
        sideBarStartX = Screen.width;
        sideBarEndX = Screen.width - sideBarWidth;
        sideBarX = sideBarStartX;
        //HQ END
    }

    void Update()
    {
        if (hqScript.isActive) isSlidingIn = true;
        else isSlidingOut = true;
    }

    void OnGUI()
    {
        MenuHUD();

        HQGUi();

        //GUI.Box(new Rect(menuTopBarX, menuTopBarY, menuTopBarWidth, menuTopBarHeight),menuTopBarBackground);
    }


    #region HQGUI - Bauen von Einheiten

    void HQGUi()
    {
        GUILayout.BeginArea(new Rect(sideBarX, sideBarY, sideBarWidth, sideBarHeight));

        GUI.Box(new Rect(0, 0, sideBarWidth, sideBarHeight), "");

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

                GUIContent cont = GetHQGUIContent(unit);

                Color color = buttonStyle.normal.textColor;
                if (unit == hqScript.unitWorkingOn)
                {
                    float workingProcess = 0;
                    workingProcess = hqScript.unitWorkingProcess;
                    GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, workingProcess / 100f);
                    cont.text += "\n(" + Convert.ToInt32(workingProcess).ToString() + "%)";
                }
                else
                {
                    GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, 1);
                }

                if (tmpIcon != null) { GUI.DrawTexture(cellRect, tmpIcon); }

                if (hqScript.isUnitWorking)
                {
                    GUI.Label(cellRect, cont, buttonStyle);
                }
                else
                {
                    if (GUI.Button(cellRect, cont, buttonStyle))
                    {
                        hqScript.isUnitWorking = true;
                        hqScript.unitWorkingOn = unit;
                        hqScript.unitWorkingProcess = 0;
                    }
                }
                unit++;
            }
        }

        //GUIResearchBuilding();

        //Tooltip Box
        Rect toolTipRect = new Rect(10, 10 + (10 + buttonSizeHeight), buttonSizeWidth + 10 + buttonSizeWidth, buttonSizeHeight);
        GUI.Box(toolTipRect, "");
        GUI.Label(toolTipRect, GUI.tooltip);

        GUILayout.EndArea();


    }

    private GUIContent GetHQGUIContent(int unit)
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

    private void Sliding()
    {
        if (isSlidingIn)
        {
            if (sideBarX > sideBarEndX) sideBarX = Mathf.Clamp(sideBarX - slidingSpeed * Time.deltaTime, sideBarEndX, sideBarStartX);
            else
            {
                isSlidingIn = false;
                sideBarX = sideBarEndX;
            }
        }
        if (isSlidingOut)
        {
            if (sideBarX <= sideBarStartX) sideBarX = Mathf.Clamp(sideBarX + slidingSpeed * Time.deltaTime, sideBarEndX, sideBarStartX);
            else
            {
                isSlidingOut = false;
                sideBarX = sideBarStartX;
            }
        }
    }

    #endregion

    #region MenuTop - Geld, Optionen etc.

    void MenuHUD()
    {
        float menuTopBarWidth = 300f;
        float menuTopBarHeight = 30f;
        float menuTopBarX = Screen.width / 2 - menuTopBarWidth / 2;
        float menuTopBarY = 0f;

        GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
        //labelStyle.alignment = TextAnchor.MiddleRight;
        labelStyle.normal.textColor = Color.black;
        labelStyle.alignment = TextAnchor.MiddleLeft;

        GUILayout.BeginArea(new Rect(menuTopBarX, menuTopBarY, menuTopBarWidth, menuTopBarHeight));

        GUI.DrawTexture(new Rect(0, 0, menuTopBarWidth, menuTopBarHeight), menuTopBarBackground, ScaleMode.ScaleToFit);

        GUI.Button(new Rect(60, 5, 80, 20), "Menü");
        GUI.Label(new Rect(menuTopBarWidth / 2 + 10, 0, 45, 30), "Credits", labelStyle);
        GUI.Label(new Rect(menuTopBarWidth / 2 + 10 + 45 + 10, 0, 50, 30), string.Format("{0:0,0}", GameProperties.credits), labelStyle);


        GUILayout.EndArea();
    }

    public static NumberFormatInfo GetGlobalNumberFormat()
    {
        NumberFormatInfo NumberFormat = (NumberFormatInfo)NumberFormatInfo.CurrentInfo.Clone();

        NumberFormat.CurrencySymbol = "";
        NumberFormat.CurrencyDecimalSeparator = ",";
        NumberFormat.CurrencyGroupSeparator = ".";
        NumberFormat.NumberDecimalDigits = 0;

        return NumberFormat;
    }
    #endregion

}
