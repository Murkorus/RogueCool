using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    // Data storage for different stats. Both for enemies and the player.
    public bool autoStats;

    public float maxHealth = 100;
    public float health = 100;
    public float damage = 5;
    public float speed = 3;
    
    private void Start()
    {
        if (autoStats)
        {
            if (gameObject.tag == "Player")
            {
                maxHealth = 100;
                health = maxHealth;
                damage = 8;
            }
            else if (gameObject.tag == "Enemy")
            {
                if (gameObject.name.Contains("Bastard"))
                {
                    maxHealth = 60;
                    health = maxHealth;
                    damage = 5;
                }
                if (gameObject.name.Contains("Roller"))
                {
                    maxHealth = 40;
                    health = maxHealth;
                    damage = 4;
                }
            }
        }
    }

    private void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
