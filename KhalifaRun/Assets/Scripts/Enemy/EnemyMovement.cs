using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Stats stats;
    private Stats targetStats;
    public float speed;
    public Transform target;
    public float minimumDistance;
    private float timePassed = 0f;


    private void Start()
    {
        stats = GetComponent<Stats>();
        targetStats = target.GetComponent<Stats>();
    }
    private void BastardAttack()
    {
        targetStats.health -= stats.damage;
        Debug.Log("Attacked");
    }


    private void Update()
    {
        if (gameObject.name.Contains("Bastard"))
        {
            if (Vector2.Distance(transform.position, target.position) > minimumDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            else
            {
                if (timePassed > 1.3f || timePassed == 0f)
                {
                    BastardAttack();
                    timePassed = 0f;
                }
                timePassed += Time.deltaTime;
            }
        }
        else if (gameObject.name.Contains("Scared Guy"))
        {
            if (Vector2.Distance(transform.position, target.position) > minimumDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
            }
            else
            {
                // Attack code
            }
        }
    }
}
