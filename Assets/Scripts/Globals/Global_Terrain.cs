using UnityEngine;
using System.Collections;

public class Global_Terrain: MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        DeselectAll();        
    }

    public static void DeselectAll()
    {
        foreach (GameObject go in GameObject.FindSceneObjectsOfType(typeof(GameObject)))
        {
            //Debug.Log(go.name);

            HQ_GUI scriptHauptHaus = go.GetComponent<HQ_GUI>();
            if (scriptHauptHaus != null) { scriptHauptHaus.SetInActive(); }

            //Einheit scriptEinheit = go.GetComponent<Einheit>();
            //if (scriptEinheit != null) { scriptEinheit.SetInActive(); }

            //UnitMain scriptUnitMain = go.GetComponent<UnitMain>();
            //if (scriptUnitMain != null) { scriptUnitMain.SetInActive(); }
            

        }
    }
}
