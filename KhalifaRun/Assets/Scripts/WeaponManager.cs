using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Weapon_template weapon;
    public GameObject[] Weapons;
    private float _distance = 10;
    public float pickupDistance;

    public bool haveGun, haveMelee;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        Weapons = GameObject.FindGameObjectsWithTag("Item_Weapon");
        
        if (ClosestWeapon() == null)
        {
            return;
        }
        
        print(Vector2.Distance(transform.position,ClosestWeapon().transform.position));
        if (Vector2.Distance(transform.position, ClosestWeapon().transform.position) <= pickupDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                weapon = ClosestWeapon().GetComponent<Item_Weapon>().Weapon;
                GetWeapon();
                Destroy(ClosestWeapon());
            }
        }
    }

    GameObject ClosestWeapon()
    {
        for (int i = 0; i < Weapons.Length; i++)
        {
            _distance = Vector2.Distance(transform.position, Weapons[i].transform.position);
            
            if (_distance <= pickupDistance)
            {
                return Weapons[i];
            }
        }
        return null;
    }

    // Update is called once per frame
        void GetWeapon()
        {
            switch (weapon.TypeWeapon)
            {
                case Weapon_template.Type.Gun:
                    GotGun();
                    break;
                case Weapon_template.Type.Melee:
                    GotMelee();
                    break;
            }
        }
        public void GotGun()
        {
            haveGun = true;
        }

        public void GotMelee()
        {
            haveMelee = true;
        }
}
