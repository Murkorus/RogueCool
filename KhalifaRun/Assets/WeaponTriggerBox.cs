using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class WeaponTriggerBox : MonoBehaviour
{
    public Weapon_template weapon;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Stats>().health -= weapon.damage;
            print("HEYYYYYYY");
        }
    }
    
}
