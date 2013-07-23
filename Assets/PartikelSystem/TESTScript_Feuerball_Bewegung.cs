using UnityEngine;
using System.Collections;

public class TESTScript_Feuerball_Bewegung : MonoBehaviour {
    public float moveXTimes = 50;
    public float movedXTimes = 0;
    public float moveStep = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {

        if (movedXTimes == 0) { moveStep = 0.5f; }
        else if (movedXTimes == moveXTimes) { moveStep = -0.5f; }

        movedXTimes += moveStep;

        Vector3 currentPosition = gameObject.transform.position;
        currentPosition.x += moveStep;

        gameObject.transform.position = currentPosition;

    }

}
