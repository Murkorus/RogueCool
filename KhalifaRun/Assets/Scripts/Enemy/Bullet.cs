using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // This script gets disabled automatically when instantiated and I don't know why D:
    private Transform target;
    private float damage;
    private float speed = 5;
    private Vector3 stopAtPoint;
    private void Awake()
    {
        target = GetComponentInParent<AIDestinationSetter>().target;
        damage = GetComponentInParent<Stats>().damage;
        stopAtPoint = target.position;

        StartCoroutine(DestoryBullet());
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,stopAtPoint,speed*Time.deltaTime);
        if (transform.position == stopAtPoint)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(1.05f, 1.25f);
            gameObject.GetComponent<AudioSource>().Play();
            col.gameObject.GetComponent<Stats>().health -= damage;
            col.gameObject.GetComponent<HealthScript>().TakingDamage(1);
            Destroy(gameObject);
        }
    }


    IEnumerator DestoryBullet()
    {
        yield return new WaitForSeconds(3f) ;
        Destroy(gameObject);
    }
}
