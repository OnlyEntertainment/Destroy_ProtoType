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
        public bool isUnitWorking = false;
        public int unitWorkingOn = 9;
        public float unitWorkingProcess = 0;
        public float unitWorkingStep = 10;

        public GameObject[] Prefabs = new GameObject[Enum.GetNames(typeof(GameProperties.VEHICLES)).Length];


        void Awake()
        {
            collectionPoint = gameObject.transform.position;
            initColor = gameObject.renderer.material.color;
        }

        void Update()
        {
            
            if (isActive) {gameObject.renderer.material.color = selectedColor;}
        

            if (isActive && Input.GetMouseButtonDown(1) && Physics.Raycast(Camera.mainCamera.ScreenPointToRay(Input.mousePosition), out hit_MouseCursor, Mathf.Infinity, 512/*9: Weg collider*/))
            {
                collectionPoint = hit_MouseCursor.point;
            }

            if (isUnitWorking)
            {
                unitWorkingProcess += unitWorkingStep * Time.deltaTime;
                if (unitWorkingProcess >= 100)
                {
                    if (Prefabs[unitWorkingOn - 1] != null)
                    {
                        GameObject go = (GameObject)Instantiate(Prefabs[unitWorkingOn - 1], gameObject.transform.position, gameObject.transform.rotation);
                        go.transform.localScale = new Vector3(2, 2, 2);
                        Unit goUnit = go.GetComponent<Unit>();
                        goUnit.ownUnit_OnAction = "walk";
                        goUnit.Unit_GoToPoint(collectionPoint);

                        //goUnit.ownUnit_TargetPoint = collectionPoint;
                        //goUnit.ownUnit_OnAction = "walk";
                        //goUnit.ownUnit_OnAction = "walk";
                        //goUnit.ownUnit_OnAction = "walk";
                        //goUnit.ownUnit_OnAction = "walk";
                        //goUnit.ownUnit_OnAction = "walk";
                    }

                    //foreach (GameObject tempCurrentSelectedUnit in currentSelectedUnits)
                    //{
                    //tempCurrentSelectedUnit.GetComponent<Unit>().Unit_GoToPoint(hit_MouseCurser .point);
                    //}
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