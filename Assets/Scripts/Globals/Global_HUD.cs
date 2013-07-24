using UnityEngine;
using System.Collections;
using System.Globalization;

public class Global_HUD : MonoBehaviour {
    private float menuTopBarWidth = 300f;
    private float menuTopBarHeight = 30f;
    private float menuTopBarX;
    private float menuTopBarY = 0f;

    public Texture2D menuTopBarBackground;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    menuTopBarX = Screen.width / 2 - menuTopBarWidth / 2;
	}

    void OnGUI()
    {
        GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
        //labelStyle.alignment = TextAnchor.MiddleRight;
        labelStyle.normal.textColor = Color.black;
        labelStyle.alignment = TextAnchor.MiddleLeft;

        GUILayout.BeginArea(new Rect(menuTopBarX,menuTopBarY,menuTopBarWidth,menuTopBarHeight));

        GUI.DrawTexture(new Rect(0,0,menuTopBarWidth,menuTopBarHeight),menuTopBarBackground,ScaleMode.ScaleToFit);

        GUI.Button(new Rect(60, 5, 80, 20), "Menü");
        GUI.Label(new Rect(menuTopBarWidth / 2 + 10, 0, 45, 30), "Credits",labelStyle);
        GUI.Label(new Rect(menuTopBarWidth / 2 + 10+45+10, 0, 50, 30), string.Format("{0:0,0}", GameProperties.credits) , labelStyle);
        

        GUILayout.EndArea();


        //GUI.Box(new Rect(menuTopBarX, menuTopBarY, menuTopBarWidth, menuTopBarHeight),menuTopBarBackground);
    }


    public static NumberFormatInfo GetGlobalNumberFormat()
    { NumberFormatInfo NumberFormat = (NumberFormatInfo)NumberFormatInfo.CurrentInfo.Clone();

       NumberFormat.CurrencySymbol = "";
       NumberFormat.CurrencyDecimalSeparator = ",";
       NumberFormat.CurrencyGroupSeparator = ".";
       NumberFormat.NumberDecimalDigits = 0;

       return NumberFormat;


    }
}
