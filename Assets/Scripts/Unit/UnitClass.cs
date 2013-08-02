using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

    class UnitClass:MonoBehaviour
    {
        //public enum DamageType {Normal};
        //public enum Properties { Health, Armor, AttackGround,AttackAir,AttackSpeed,Speed,BuildingSpeed,Cost,Range};

        private enum ATTACKTYPE { Ground, Air };

        private Dictionary<GameProperties_CT.PROPERTIES, int> level = new Dictionary<GameProperties_CT.PROPERTIES, int>();

        //private enum Properties GameProperties_CT.Properties;

        public GameObject prefab = null;

        public GameProperties_CT.VEHICLES type = GameProperties_CT.VEHICLES.Jeep;
        public String name = "Panzer";
        public String toolTip = "tooltip";

        public float[] maxHealth = new float[3] {100,200,300};
        private float curHealth;

        public float[] armor = new float[3] { 10, 15, 20};
        public float[] attackGround = new float[3] { 40, 60, 80};
        public float[] attackAir = new float[3] { 0, 0, 0 };
        public float[] attackSpeed = new float[3] { 1, 2, 3 };
        public float[] speed = new float[3] { 80, 90, 100};
        public float[] buildingSpeed = new float[3] { 2, 4, 6 };
        public int[] cost = new int[3] { 500, 450, 400};
        public float[] range = new float[3] { 10, 15, 20 };

        


        private void Awake()
        {
            curHealth = maxHealth[0];
            int lengthOfEnum = Enum.GetNames(typeof(GameProperties_CT.PROPERTIES)).Length;

            for (int i = 0; i <= lengthOfEnum; i++)
            {
                level[(GameProperties_CT.PROPERTIES)i] = 0;
            }

        }


        public void RefreshAllLevels()
        {
            int lengthOfEnum = Enum.GetNames(typeof(GameProperties_CT.PROPERTIES)).Length;

            for (int i = 0; i <= lengthOfEnum; i++)
            {
                level[(GameProperties_CT.PROPERTIES)i] = GameProperties_CT.levelOfAllUnits[type][(GameProperties_CT.PROPERTIES)i];                   
            }
        }

        //public void GetDamage(float damage, DamageType damageType)
        public void GetDamage(float damage)
        {
            damage = Mathf.Clamp(damage - armor[level[GameProperties_CT.PROPERTIES.Armor]], 0, damage);
            curHealth = Mathf.Clamp(curHealth - damage, 0, curHealth);

            if (curHealth <= 0)
            {
                Destroy(gameObject);
            }
        }




            


    }
