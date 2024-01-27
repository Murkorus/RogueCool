using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForPlayer : MonoBehaviour
{
    private AIPath pathfinder;
    private GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        pathfinder = GetComponent<AIPath>();
        pathfinder.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position,player.transform.position) < 20)
        {
            pathfinder.enabled = true;
        }
    }
}
