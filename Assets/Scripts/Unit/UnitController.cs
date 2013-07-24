using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[RequireComponent(typeof(Camera))]

public class UnitController : MonoBehaviour {
	
	/*
	Temporär(Debug):
		-Start()
			37		Rect des Menüs
		-OnGUI()
			98-108	Zeichnen des Menüs
	*/
	
	public Rect GUI_menuRect = new Rect(0,0,0,0);
	
	List<GameObject> currentSelectedUnits = new List<GameObject>(); //Liste aller selektierten Einheiten
	public Transform allUnitsHolder; //Parent aller Einheiten der Szene
	
	RaycastHit hit_MouseCurser; //Informationsspeicher der Raycasts
	
	bool windowSelection = false; //Selektion durch Fenster
	Vector3 windowSelection_Start; //Anfangspunkt vom Fenster
	
	bool doubleClick; //Doppelklick in diesem Frame gültig
	public float doubleClick_TimeRange; //Zeit, in welcher ein zweiter Klick als Doppelklick gillt
	float doubleClick_time = 0; //letzter Doppelklick

    Camera camera;

	//*******************************************
	//BUILD-IN***********************************
	//*******************************************

    void Awake()
    {
        camera = Camera.mainCamera;
    }

	void Start ()
	{
		GUI_menuRect = new Rect(0, Screen.height -100, Screen.width, 100);
	}
	
	void Update ()
	{
		if (!GUI_menuRect.Contains(ChangeCoordinates(Input.mousePosition)))
		{
			//Einzelselektion
			if (Input.GetMouseButtonDown(0))
			{
				windowSelection = true;
				windowSelection_Start = Input.mousePosition;
				
				//Test für Doppelklick
				doubleClick = Time.time - doubleClick_time <= doubleClick_TimeRange;
				doubleClick_time = Time.time;
				
				if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit_MouseCurser, Mathf.Infinity, 256/*8: Einheiten collider*/))
				{
					ResetSingleSelection(hit_MouseCurser.collider.gameObject);
					
					//Mehrfachselektion durch Doppelklick
					if (doubleClick)
					{
						ResetMultiSelection(hit_MouseCurser.collider.gameObject);
					}
				}
				else
				{
					ResetSingleSelection(null);
				}
			}
			
			//Selektierte Einheiten befehligen
			if (Input.GetMouseButtonDown(1))
			{
				if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit_MouseCurser, Mathf.Infinity, 512/*9: Weg collider*/) && currentSelectedUnits.Count > 0)
				{	
					foreach (GameObject tempCurrentSelectedUnit in currentSelectedUnits)
					{
						tempCurrentSelectedUnit.GetComponent<Unit>().Unit_GoToPoint(hit_MouseCurser.point);
					}
				}
			}
		}//ENDE (!menuRect.Contains(Mouse))
		
		//Mehrfachselektion durch Fenster
		if (Input.GetMouseButtonUp(0) && windowSelection && !doubleClick)
		{
			ResetMultiSelection(null);
		}
		
		if (!Input.GetMouseButton(0))
		{
			windowSelection = false;
		}
		
	}//ENDE Update()
	
	void OnGUI ()
	{
		if (windowSelection)
		{
			GUI.Box(
				AbsRect(
					new Vector2(windowSelection_Start.x, ChangeCoordinates(windowSelection_Start).y),
					new Vector2(Input.mousePosition.x, ChangeCoordinates(Input.mousePosition).y)
				), "");
		}
		
		GUI.Box(GUI_menuRect, "Menue; hier durch-selektieren nich moeglich(auch mit multi selection)");
		if (GUI.Button(new Rect(0,Screen.height -40, 80, 40), "Beenden")) {Application.Quit();}
	}//ENDE OnGUI()
	
	//*******************************************
	//CUSTOM*************************************
	//*******************************************
	
	void AddUnitToSelectionArray (GameObject theUnit)
	{
		currentSelectedUnits.Add(theUnit);
		theUnit.GetComponent<Unit>().Select();
	}
	
	void ResetSingleSelection (GameObject newUnit)
	{
		if (currentSelectedUnits.Count > 0)
		{
			foreach (GameObject tempUnitToDeselect in currentSelectedUnits)
			{
				tempUnitToDeselect.GetComponent<Unit>().Deselect();
			}
			currentSelectedUnits.Clear();
		}
		if (newUnit)
		{
			AddUnitToSelectionArray(newUnit);
		}
	}
	
	void ResetMultiSelection (GameObject typeLikeUnit)
	{
		//durch Doppelklick
		if (typeLikeUnit)
		{
			foreach (Transform tempUnit in allUnitsHolder)
			{
				//Typvergleich
				if (tempUnit.GetComponent<Unit>().unitType == typeLikeUnit.GetComponent<Unit>().unitType)
				{
					Vector3 tempTempUnitWorldToScreen = camera.WorldToScreenPoint(tempUnit.position);
					//Test ob sich die Einheit im Bildschirm befindet (sichtbar)
					if (tempTempUnitWorldToScreen.z >= 0 && new Rect(0,0,Screen.width,Screen.height).Contains(tempTempUnitWorldToScreen ) )
					{
						//Test ob sich die Einheit nicht hinter GUI befindet (sichtabr)
						if (!GUI_menuRect.Contains(ChangeCoordinates(tempTempUnitWorldToScreen )) )
						{
							AddUnitToSelectionArray(tempUnit.gameObject);
						}
					}
				}
			}
		}
		
		//durch Fenster
		else
		{
			Rect tempWindowSelectionRect = AbsRect(
				new Vector2(windowSelection_Start.x, windowSelection_Start.y),
				new Vector2(Input.mousePosition.x, Input.mousePosition.y)
			);
			
			//Angeklickte Einheit noch mit einbeziehen
			if (currentSelectedUnits.Count > 0)
			{
				if (currentSelectedUnits[0] == hit_MouseCurser.collider.gameObject)
				{
					ResetSingleSelection(currentSelectedUnits[0]);
				}
			}
			
			foreach (Transform tempUnit in allUnitsHolder)
			{
				Vector3 tempTempUnitWorldToScreen = camera.WorldToScreenPoint(tempUnit.position);
				//Test ob sich die Einheit im Bildschirm befindet (sichtbar)
				if (tempTempUnitWorldToScreen.z >= 0 && new Rect(0,0,Screen.width,Screen.height).Contains(tempTempUnitWorldToScreen ) )
				{
					//Test ob sich die Einheit innerhalb des SelectionWindows und nicht hinter GUI (sichtbar) befindet
					if (tempWindowSelectionRect.Contains(tempTempUnitWorldToScreen) && !GUI_menuRect.Contains(ChangeCoordinates(tempTempUnitWorldToScreen )) )
					{
						AddUnitToSelectionArray(tempUnit.gameObject);
					}
				}
			}
		}
	}
	
	//GUI-Koordinaten und Screen-Koordinaten austauschen
	Vector3 ChangeCoordinates (Vector3 oldCoordinates)
	{
		return new Vector3(oldCoordinates.x, Screen.height -  oldCoordinates.y, oldCoordinates.z);
	}
	
	//Absolutes Rect (0 oben links) 
	Rect AbsRect (Vector2 GUIPoint1, Vector2 GUIPoint2)
	{
		if (GUIPoint1.x > GUIPoint2.x)
		{
			Switch2Floats(ref GUIPoint1.x, ref GUIPoint2.x);
		}
		if (GUIPoint1.y > GUIPoint2.y)
		{
			Switch2Floats(ref GUIPoint1.y, ref GUIPoint2.y);
		}
		return new Rect(GUIPoint1.x, GUIPoint1.y, GUIPoint2.x - GUIPoint1.x, GUIPoint2.y - GUIPoint1.y);
	}
	
	void Switch2Floats (ref float float1, ref float float2)
	{
		float temp_ChangeFloat = float1;
		float1 = float2;
		float2 = temp_ChangeFloat;
	}
}
