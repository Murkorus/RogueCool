using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapons", menuName = "SpecialMoves", order = 1)]
public class SpecialMove_Template : ScriptableObject
{
    public Weapon WeaponObject;
    public GameObject instasiate;
    public Vector3 newWeaponSize;
    
    public enum Weapon
    {
        linial,
        lommelygte
    }
}
