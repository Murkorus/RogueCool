using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapons", menuName = "Weapons", order = 0)]
public class Weapon_template : ScriptableObject
{
    public enum Type
    {
        Melee,
        Gun
    }
    public string weaponName;
    public Type TypeWeapon;
    public int damage;

    public SpecialMove_Template Special;
    [Header("Melee")]
    public Sprite Melee_Sprite;
    public int attackSpeed;
    public int windup;
    public float Weight;
    
    [Header("Gun")] 
    public int ShootSpeed;
    public int Ammo;
    public int reloadespeed;
    public Sprite bullet;
    public Sprite Gun_sprite;
}
