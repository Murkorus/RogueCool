using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stats : MonoBehaviour
{
    // Data storage for different stats. Both for enemies and the player.
    public bool autoStats;

    public float maxHealth = 100;
    public float health = 100;
    public float damage = 5;
    public float speed = 3;
    public float knockback = 0;
    public float attackSpeed = 1; // Seconds between each attack
    public bool ranged;
    public float range;
    private AIPath pathfindingScript;

    private void Start()
    {
        pathfindingScript = GetComponent<AIPath>();
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
                    pathfindingScript.maxSpeed = 4;
                    knockback = 0.3f;
                    attackSpeed = 1;
                    ranged = false;
                    range = 2.5f;
                }
                if (gameObject.name.Contains("Roller"))
                {
                    maxHealth = 40;
                    health = maxHealth;
                    damage = 4;
                    pathfindingScript.maxSpeed = 5;
                    knockback = 0.9f;
                    attackSpeed = 1.3f;
                    ranged = false;
                    range = 2;
                }
                if (gameObject.name.Contains("Spitter"))
                {
                    maxHealth = 28;
                    health = maxHealth;
                    damage = 11;
                    pathfindingScript.maxSpeed = 3;
                    knockback = 0.4f;
                    attackSpeed = 1.1f;
                    ranged = true;
                    range = 10;
                }
            }
        }
        else if (gameObject.tag == "Enemy")
        {
            pathfindingScript.maxSpeed = speed;
        }
    }

    private void Update()
    {
        if(health <= 0)
        {
            SceneManager.LoadScene("DeathScreen");
        }
    }
}
