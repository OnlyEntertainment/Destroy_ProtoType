using UnityEngine;
using System.Collections;
using System;

public class KamerSteuerung : MonoBehaviour
{

    public int smoothToStdState = -1;      	//Status des Rückfluges
    // -> -1 = nicht aktiv  	0 = aktiv   	1 & 2 = aktiv, Anzahl der abgeschlossenen Aktionen
    bool rotPerMouseActive = false;
    public Vector2 rotPerMouseStartPos;

    /// <summary>
    /// von Unity aufgerufene Initialisierungs-Funktion
    /// </summary>
    void Start()
    {

    }

    /// <summary>
    /// von Unity pro Frame aufgerufene Funktion für allg. Aktualisierungen
    /// </summary>
    void Update()
    {
        if (smoothToStdState == -1)
        {
            Vector3 NewCamPos = gameObject.transform.position;
            float RotY = gameObject.transform.eulerAngles.y;

            //== Bewegung in wagerechten Koordinaten
            //nach vorne
            if (Input.GetKey(KeyCode.W) ||
                (rotPerMouseActive == false && (Input.mousePosition.y > (Screen.height - (Screen.height / 10)))))
            {
                NewCamPos.x += (float)Math.Sin(RotY * Math.PI / 180) * 0.5f;
                NewCamPos.z += (float)Math.Cos(RotY * Math.PI / 180) * 0.5f;
            }
            //nach hinten
            if (Input.GetKey(KeyCode.S) ||
                    (rotPerMouseActive == false && (Input.mousePosition.y < (Screen.height / 10))))
            {
                NewCamPos.x -= (float)Math.Sin(RotY * Math.PI / 180) * 0.5f;    
                NewCamPos.z -= (float)Math.Cos(RotY * Math.PI / 180) * 0.5f;
            }
            //nach links
            if (Input.GetKey(KeyCode.A) ||
                (rotPerMouseActive == false && (Input.mousePosition.x < (Screen.width / 10))))
            {
                NewCamPos.x += (float)Math.Sin((RotY - 90) * Math.PI / 180) * 0.5f;
                NewCamPos.z += (float)Math.Cos((RotY - 90) * Math.PI / 180) * 0.5f;
            }
            //nach rechts
            if (Input.GetKey(KeyCode.D) ||
                (rotPerMouseActive == false && (Input.mousePosition.x > (Screen.width - (Screen.width / 10)))))
            {
                NewCamPos.x += (float)Math.Sin((RotY + 90) * Math.PI / 180) * 0.5f;
                NewCamPos.z += (float)Math.Cos((RotY + 90) * Math.PI / 180) * 0.5f;
            }

            //== Rotation
            //nach rechts
            if (Input.GetKey(KeyCode.Q))
            {
                gameObject.transform.Rotate(0, -1, 0, Space.World);
            }
            //nach links
            if (Input.GetKey(KeyCode.E))
            {
                gameObject.transform.Rotate(0, 1, 0, Space.World);
            }
            //per Maus (bei gedrücktem Scrollrad)
            if (rotPerMouseActive == true)
            {
                if (Input.GetKey(KeyCode.Mouse1))
                {
                    //auf der x-Achse
                    if (Input.GetKey(KeyCode.LeftControl))
                        gameObject.transform.Rotate(((rotPerMouseStartPos.x - Input.mousePosition.x) +
                            (rotPerMouseStartPos.y - Input.mousePosition.y)) * 0.003f, 0, 0, Space.World);
                    //auf der z-Achse
                    else if (Input.GetKey(KeyCode.LeftAlt))
                        gameObject.transform.Rotate(0, 0, ((rotPerMouseStartPos.x - Input.mousePosition.x) +
                            (rotPerMouseStartPos.y - Input.mousePosition.y)) * 0.003f, Space.World);
                    //auf der y-Acse
                    else
                        gameObject.transform.Rotate(0, -((rotPerMouseStartPos.x - Input.mousePosition.x) +
                            (rotPerMouseStartPos.y - Input.mousePosition.y)) * 0.0075f, 0, Space.World);
                }
                else
                    rotPerMouseActive = false;
            }
            else
                if (Input.GetKey(KeyCode.Mouse1))
                {
                    rotPerMouseActive = true;
                    rotPerMouseStartPos = Input.mousePosition;
                }

            //== Bewegung in vertikalen Koordinaten
            //hoch
            if (Input.GetKey(KeyCode.PageUp))
            {
                NewCamPos.y += 0.25f;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                NewCamPos.y += 1;
            }

            if (NewCamPos.y > 30)
                NewCamPos.y = 30;

            //runter
            if (Input.GetKey(KeyCode.PageDown))
            {
                NewCamPos.y -= 0.25f;
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                NewCamPos.y -= 1;
            }

            if (NewCamPos.y < 15)
                NewCamPos.y = 15;

            //== Positions-Änderung anwenden
            gameObject.transform.position = NewCamPos;

            //== Test, ob Pos1 gedrückt ist 
            //  	(-> Aktivierung des "sanften Rückfluges" zu der Standart-Rotation & -Höhe)
            if (Input.GetKey(KeyCode.Home))
                smoothToStdState = 0;
        }
        else
        {
            smoothToStdState = 0;

            //== Y-Rotation zurück zu 0°
            if ((gameObject.transform.eulerAngles.y < 355) && (gameObject.transform.eulerAngles.y > 180))
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(75, gameObject.transform.eulerAngles.y + 2, 0));
            else if ((gameObject.transform.eulerAngles.y > 5) && (gameObject.transform.eulerAngles.y <= 180))
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(75, gameObject.transform.eulerAngles.y - 2, 0));
            else
            {
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(75, 0, 0));
                smoothToStdState += 1;
            }

            //== Y-Position zurück zu 25
            if (gameObject.transform.position.y != 25)
            {
                Vector3 NewCamPos = gameObject.transform.position;

                if (NewCamPos.y > 25)
                    NewCamPos.y -= 0.25f;
                if (NewCamPos.y < 25)
                    NewCamPos.y += 0.25f;

                gameObject.transform.position = NewCamPos;
            }
            else
                smoothToStdState += 1;

            //== Test ob Rückflug abgeschlossen ist
            if (smoothToStdState == 2)
                smoothToStdState = -1;
        }
    }
}