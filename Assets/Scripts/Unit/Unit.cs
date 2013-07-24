using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	
	public string unitType;
	
	public float ownUnit_WalkSpeed; //Bewegungsgeschwindigkeit der Einheit
	
	public float ownUnit_AreaRadius; //Radius des SelectionRing-Projektors
	
	public string ownUnit_OnAction; //derzeitige Aktion der Einheit
	public Vector2 ownUnit_TargetPoint; //für "walk"-Aktion; Zielort
	
	public GameObject selectionProjector_public; //Prefab des SelectionRing-Projektors
	GameObject selectionProjector_own;
	
	void Awake ()
	{
		ownUnit_OnAction = "idle";
	}
	
	void Update ()
	{
		switch(ownUnit_OnAction)
		{
		case "walk":
                //Debug.Log("Gehe in Walk");
			//höchstens eine Frame-Bewegung vom Ziel
			if ((ownUnit_TargetPoint - new Vector2(transform.position.x, transform.position.z)).magnitude >= ownUnit_WalkSpeed*Time.deltaTime)
			{
					transform.LookAt(new Vector3(ownUnit_TargetPoint.x, transform.position.y, ownUnit_TargetPoint.y));
					transform.Translate(Vector3.forward * ownUnit_WalkSpeed *Time.deltaTime);
					transform.position = new Vector3 (transform.position.x, Terrain.activeTerrain.SampleHeight(transform.position), transform.position.z);	
			}
			else
			{
				ownUnit_OnAction = "idle"; //nach Ankommen auf "idle"-Aktion
			}
		break;
		}
	}
	
	public void Select () //Selektierung mit Projektor anzeigen
	{
		if (selectionProjector_own == null)
		{
			selectionProjector_own = Instantiate(selectionProjector_public, transform.position + new Vector3(0f, 1f, 0f)/*Abhängig von falloff-Textur*/, Quaternion.Euler(90,0,0)) as GameObject;
			selectionProjector_own.transform.parent = transform;
			selectionProjector_own.GetComponent<Projector>().orthographicSize = ownUnit_AreaRadius;
		}
	}
	
	public void Deselect () //Projektor zerstören
	{
		if (selectionProjector_own)
		{
			Destroy(selectionProjector_own);
			selectionProjector_own = null;
		}
	}
	
	public void Unit_GoToPoint (Vector3 newTargetPoint) //in Bewegung setzen, falls keine vorrangige Aktion läuft
	{
        Debug.Log("Erhalte Befehle ("+ownUnit_OnAction+") --> Fahre zu " + newTargetPoint);
		if (ownUnit_OnAction != "Aktion mit Vorrang")
		{
			ownUnit_OnAction = "walk";
			ownUnit_TargetPoint = new Vector2(newTargetPoint.x, newTargetPoint.z);
		}
	}
	
	void OnDrawGizmos ()
	{
		Gizmos.DrawWireSphere(transform.position, ownUnit_AreaRadius ); //Einschätzen des Radius des SelectionRing-Projektors
	}
}
