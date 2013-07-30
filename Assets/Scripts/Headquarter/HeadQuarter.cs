using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

    class HeadQuarter :  MonoBehaviour
    {

        public bool isActive = false;

        private Color initColor;
        private Color selectedColor = Color.cyan;
        private Color mouseOverColor = Color.blue;

        //Letzter Einheitensammelpunkt
        private RaycastHit hit_MouseCursor; //Informationsspeicher der Raycasts
        public Vector3 collectionPoint = Vector3.zero;

        //Einheitenbau
        public const int untiWorkingLineMax = 5;
        public bool isUnitWorking = false;
        public int unitWorkingOn = 9;
        public float unitWorkingProcess = 0;
        public float unitWorkingSpeed = 10;
        public int unitWorkingLine = 0;

        public GameObject[] Prefabs = new GameObject[Enum.GetNames(typeof(GameProperties.VEHICLES)).Length];


        void Awake()
        {
            collectionPoint = gameObject.transform.position;
            initColor = gameObject.renderer.material.color;
        }

        public void ProduceUnit(int unitWorkingOn, float unitWorkingSpeed)
        {
            if (this.unitWorkingLine == 0)
            {
                this.unitWorkingLine++;
                this.unitWorkingOn = unitWorkingOn;
                this.unitWorkingSpeed = unitWorkingSpeed;
                this.unitWorkingProcess = 0;
            }
            else if (unitWorkingLine > 0 && unitWorkingOn == this.unitWorkingOn && untiWorkingLine < uni)
            {
                this.unitWorkingLine++;
            }
        }



        void Update()
        {
        
            //Farbe anpassen wenn HQ Aktiv
            if (isActive) {gameObject.renderer.material.color = selectedColor;}
        
            //Wenn Aktiv und Rechtsklick gemacht wird, dann wird neuer Sammeölpunkt gesetzt.
            if (isActive && Input.GetMouseButtonDown(1) && Physics.Raycast(Camera.mainCamera.ScreenPointToRay(Input.mousePosition), out hit_MouseCursor, Mathf.Infinity, 512/*9: Weg collider*/))
            {
                collectionPoint = hit_MouseCursor.point;
            }

            //Eigentliche Produktion der Einheiten
            if (isUnitWorking)
            {
                unitWorkingProcess += unitWorkingSpeed * Time.deltaTime;
                if (unitWorkingProcess >= 100)
                {
                    if (Prefabs[unitWorkingOn - 1] != null)
                    {
                        GameObject go = (GameObject)Instantiate(Prefabs[unitWorkingOn - 1], gameObject.transform.position, gameObject.transform.rotation);
                        go.transform.localScale = new Vector3(2, 2, 2);
                        Unit goUnit = go.GetComponent<Unit>();
                        goUnit.ownUnit_OnAction = "walk";
                        goUnit.Unit_GoToPoint(collectionPoint);
                    }

                    isUnitWorking = false;
                    unitWorkingOn = 9;
                    unitWorkingProcess = 0;


                }
            }   
        }

        private void OnMouseEnter()
        {
            gameObject.renderer.material.color = mouseOverColor;
        }

        private void OnMouseExit()
        {
            gameObject.renderer.material.color = initColor;
        }

        private void OnMouseDown()
        {
            if (!isActive)
            {                
                Global_Terrain.DeselectAll();
                isActive = true;
            }            
        }

        public void SetInActive()
        {
            if (isActive)
            {
                isActive = false;
                gameObject.renderer.material.color = initColor;
            }
        }

    }