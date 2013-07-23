using UnityEngine;
using System.Collections;

public class HQ_Research : MonoBehaviour {

    private bool showGUI = false;


    //Konstanten
    const int headerTypes = 7;
    const int detailRows = 8;
    const int detailCols = 3;
    const int detailLines = 4;

    private float researchOuterSpace = 50.0f;
    private float researchInnerSpace = 50.0f;
    private Vector2 researchScrollPosition = Vector2.zero;

    
    private float headerHeight = 50.0f;
    public  int headerSelected = 0;
    string[] headerStrings = new string[headerTypes] { "Jeep", "Leichter Panzer", "Mittlerer Panzer", "Schwerer Panzer", "Helikopter", "Jäger", "Bomber" };

    private float detailInnerSpace = 5.0f;
    private float detailHeight = 100.0f;
    private float detailWidth = 250.0f;

    
    private string[, , ,] details = new string[headerTypes, detailRows, detailCols,detailLines]; //Type, row, col, detailLine

    private float textHeight = 20.0f;
    
    

    //Styles
    GUIStyle labelUpperLeft;
    GUIStyle labelUpperRight;


	// Use this for initialization
	void Start () 
    {
        GetDetailLines();
	}
	
	// Update is called once per frame
	void Update () 
    {        
        if (Input.GetKeyDown(KeyCode.R)) showGUI = !showGUI;	
	}

    void OnGUI()
    {
        GetStyles();
        

        if (showGUI)
        {

            //DEBUG
            researchOuterSpace = 50.0f;
            researchInnerSpace = 20.0f;

            headerHeight = 50.0f;

            detailInnerSpace = 5.0f;
            detailHeight = 100f;

            textHeight = 20.0f;
            
            // 

            Rect researchBox = new Rect(researchOuterSpace, researchOuterSpace, Screen.width - (2 * researchOuterSpace), Screen.height - (2 * researchOuterSpace));


            GUILayout.BeginArea(researchBox);
            GUI.Box(new Rect(0, 0, researchBox.width, researchBox.height), "");


            //BEGIN HEADER
            Rect headerRect = new Rect(researchInnerSpace, researchInnerSpace, researchBox.width - (2 * researchInnerSpace), headerHeight);

            GUILayout.BeginArea(headerRect, "");
            GUI.Box(new Rect(0, 0, headerRect.width, headerRect.height), "");
            headerSelected = GUI.SelectionGrid(new Rect(0, 0, headerRect.width, headerRect.height), headerSelected, headerStrings, headerStrings.Length);
            //headerSelected = GUILayout.SelectionGrid(headerSelected, headerStrings, headerStrings.Length);

            GUILayout.EndArea(); 
            //END Header


            //BEGIN Detail
            Rect detailRect = new Rect(researchInnerSpace, headerRect.x+headerHeight+researchInnerSpace, researchBox.width- (2* researchInnerSpace), researchBox.height - headerHeight-3*researchInnerSpace);
            GUILayout.BeginArea(detailRect, "");
            GUI.Box(new Rect(0, 0, detailRect.width, detailRect.height), "");
            researchScrollPosition = GUI.BeginScrollView(new Rect(0, 0, detailRect.width, detailRect.height), researchScrollPosition, new Rect(0, 0, detailCols * (detailWidth + researchInnerSpace) + researchInnerSpace, detailRows * (detailHeight + researchInnerSpace) + researchInnerSpace));

            for (int row = 0; row < detailRows; row++)
            {
                for (int col = 0; col < detailCols; col++)
                {
                    DrawDetail(headerSelected,row,col);
                }
            }





            GUI.EndScrollView();
            GUILayout.EndArea(); 
            //END DETAIL

            
            GUILayout.EndArea();
        }
    }

    void DrawDetail(int type, int row, int col)
    {
        Rect boxRect = new Rect((researchInnerSpace * (col+1)) + (col * detailWidth), (researchInnerSpace * (row+1)) + (row * detailHeight), detailWidth, detailHeight);
        float innerBoxWidth = boxRect.width - 2 * detailInnerSpace;

        if (details[type, row, col, 0] != "" && details[type, row, col, 0] != null)
        {
            GUILayout.BeginArea(boxRect, "");
            GUI.Box(new Rect(0, 0, boxRect.width, boxRect.height), "");

            //1.Zeile
            GUI.Label(new Rect(detailInnerSpace, detailInnerSpace, innerBoxWidth, textHeight), details[type, row, col, 0], labelUpperLeft); //Titel
            GUI.Label(new Rect(detailInnerSpace, detailInnerSpace, innerBoxWidth, textHeight), details[type, row, col, 1] +  " C", labelUpperRight); //Credits

            //2.Zeile
            GUI.Label(new Rect(detailInnerSpace, detailInnerSpace + (1 * textHeight), innerBoxWidth, textHeight), details[type, row, col, 2]+ " min", labelUpperRight); //Zeit

            //3. Zeile (Info Text)
            GUI.Label(new Rect(detailInnerSpace, detailInnerSpace + (2 * textHeight), innerBoxWidth, 100), details[type, row, col, 3], labelUpperLeft); // Beschreibung

            GUILayout.EndArea();
        }
    }

    void GetStyles()
    {
        labelUpperLeft = new GUIStyle(GUI.skin.label);
        labelUpperLeft.alignment = TextAnchor.UpperLeft;
        //labelTopLeft.fontSize = 12;

        labelUpperRight = new GUIStyle(GUI.skin.label);
        labelUpperRight.alignment = TextAnchor.UpperRight;
    }

    void GetDetailLines()
    { 
        //Type, row, Col, DetailLine

        //Type = 0 --> Jeep
        //R1C1
        details[0, 0, 0, 0] = "Jeep - Waffen 1";
        details[0, 0, 0, 1] = "1000";
        details[0, 0, 0, 2] = "02:00";
        details[0, 0, 0, 3] = "Durch die neuen Geschoßmäntel, können die Kugeln schneller abgefeuert werden und fliegen weiter.";//(Angriffsfrequenz & Reichweite)
        //R1C2
        details[0, 0, 1, 0] = "Jeep - Waffen 2";
        details[0, 0, 1, 1] = "2000";
        details[0, 0, 1, 2] = "05:00";
        details[0, 0, 1, 3] = "Durch die neuen Geschoßmäntel, können die Kugeln schneller abgefeuert werden und fliegen weiter.";//(Angriffsfrequenz & Reichweite)
        //R1C3
        details[0, 0, 2, 0] = "Jeep - Waffen 3";
        details[0, 0, 2, 1] = "5000";
        details[0, 0, 2, 2] = "15:00";
        details[0, 0, 2, 3] = "Durch die neuen Geschoßmäntel, können die Kugeln schneller abgefeuert werden und fliegen weiter.";//(Angriffsfrequenz & Reichweite)

    }
}
