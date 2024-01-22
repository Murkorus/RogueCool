using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEditor.Rendering;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // This script gets disabled automatically when instantiated and I don't know why D:
    private Transform target;
    private float damage;
    private float speed = 5;
    private void Awake()
    {
        target = GetComponentInParent<AIDestinationSetter>().target;
        damage = GetComponentInParent<Stats>().damage;
    }

    private void Update()
    {
        Vector2.MoveTowards(transform.position,target.transform.position,speed*Time.deltaTime);
    }
}
