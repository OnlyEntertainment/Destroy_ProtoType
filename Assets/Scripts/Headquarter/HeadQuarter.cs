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