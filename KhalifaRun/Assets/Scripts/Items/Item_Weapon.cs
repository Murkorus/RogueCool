using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item_Weapon : MonoBehaviour
{
    
    // Describtion dette script er til 
    public Weapon_template Weapon;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Weapon != null)
        {
            UpdateWeapon();
        }
        else if (Weapon == null)
        {
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void UpdateWeapon()
    {
        gameObject.name = "item_" + Weapon.weaponName;
        switch (Weapon.TypeWeapon )
        {
        case Weapon_template.Type.Gun:
            gameObject.GetComponent<SpriteRenderer>().sprite = Weapon.Gun_sprite;
            break;
        case Weapon_template.Type.Melee:
            gameObject.GetComponent<SpriteRenderer>().sprite = Weapon.Melee_Sprite;
            break;
        }
    }
}
