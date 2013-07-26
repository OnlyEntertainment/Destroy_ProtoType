using UnityEngine;
using System.Collections;
using System.Globalization;

public class GlobalGUI_CT : GlobalGUI
{
    public GameObject HeadQuarter;

    public Texture2D menuTopBarBackground;

    private HeadQuarter hqScript;

    // Use this for initialization
    void Start()
    {
        hqScript = HeadQuarter.GetComponent<HeadQuarter>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnGUI()
    {
        MenuHUD();


        //GUI.Box(new Rect(menuTopBarX, menuTopBarY, menuTopBarWidth, menuTopBarHeight),menuTopBarBackground);
    }


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
}
