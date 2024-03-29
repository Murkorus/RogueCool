using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    private float timePassed = 0f;
    private AIPath pathfindingScript;
    private AIDestinationSetter destinationSetter;
    private Stats stats;
    private Stats targetStats;
    private Rigidbody2D body;
    public GameObject rangedAttackBullet;
    // Start is called before the first frame update
    void Start()
    {
        pathfindingScript = GetComponent<AIPath>();
        stats = GetComponent<Stats>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        targetStats = destinationSetter.target.GetComponent<Stats>();
        body = GetComponent<Rigidbody2D>();
    }
    /*
    // Update is called once per frame
    void Update()
    {
        if (pathfindingScript.reachedDestination) // If enemy is in range of player
        {
            if (timePassed > 1.3f || timePassed == 0f) // Every 1.3 seconds,and when the enemy gets in range of player for the first time
            {
                pathfindingScript.enabled = false; // Disable pathfinding script
                transform.position += Vector3.forward * Time.deltaTime *10; // Push the enemy slightly in the direction it's facing (Towards the player)
                targetStats.health -= stats.damage; // Deal damage
                Debug.Log("Attacked for " + stats.damage + " damage. Target has " + targetStats.health + " health left."); // Debug
                timePassed = 0f; // Reset the timer
                pathfindingScript.enabled = true; // Enable the pathfinding script again
            }
            timePassed += Time.deltaTime; // Update the timer until it reaches 1.3 seconds
        }
    }
    */
    void Update()
    {
        if (pathfindingScript.reachedDestination) // If enemy is in range of player
        {
            if (timePassed > stats.attackSpeed || timePassed == 0f) // Every 1.3 seconds,and when the enemy gets in range of player for the first time
            {
                if (!stats.ranged)
                {
                    StartCoroutine(MeleeAttack());
                }
                else
                {
                    StartCoroutine(RangedAttack());
                }
            }
            timePassed += Time.deltaTime; // Update the timer until it reaches 1.3 seconds
        }
    }



    public IEnumerator MeleeAttack()
    {
        pathfindingScript.enabled = false; // Disable pathfinding script
        transform.position += (destinationSetter.target.transform.position - transform.position).normalized * 0.5f; // Push the enemy slightly in the direction it's facing (Towards the player)
        targetStats.health -= stats.damage; // Deal damage
        destinationSetter.target.GetComponent<Rigidbody2D>().AddForce((destinationSetter.target.transform.position - transform.position).normalized * stats.knockback * 1000); // Knockback
        yield return new WaitForSeconds(0.15f);
        transform.position -= (destinationSetter.target.transform.position - transform.position).normalized * 0.5f;
        yield return new WaitForSeconds(stats.attackSpeed - 0.15f);
        timePassed = 0f; // Reset the timer
        pathfindingScript.enabled = true; // Enable the pathfinding script again
    }

    public IEnumerator RangedAttack()
    {
        pathfindingScript.enabled = false; // Disable pathfinding script
        transform.position += (destinationSetter.target.transform.position - transform.position).normalized * 0.5f; // Push the enemy slightly in the direction it's facing (Towards the player)
        //
        Instantiate(rangedAttackBullet,transform.position,transform.rotation,transform);
        // For some reason, the script on the bullet is disabled when instantiated
        //
        yield return new WaitForSeconds(0.15f);
        transform.position -= (destinationSetter.target.transform.position - transform.position).normalized * 0.5f;
        yield return new WaitForSeconds(stats.attackSpeed - 0.15f);
        timePassed = 0f; // Reset the timer
        pathfindingScript.enabled = true; // Enable the pathfinding script again
    }
}
