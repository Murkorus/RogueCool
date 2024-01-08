using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    // Data storage for different stats. Both for enemies and the player.

    public float maxHealth;
    public float health;
    public float damage;

    private void Start()
    {
        if(gameObject.tag == "Player")
        {
            maxHealth = 100;
            health = maxHealth;
            damage = 8;
        }
        else if(gameObject.tag == "Enemy")
        {
            if (gameObject.name.Contains("Bastard"))
            {
                maxHealth = 60;
                health = maxHealth;
                damage = 5;
            }
        }
    }
}
