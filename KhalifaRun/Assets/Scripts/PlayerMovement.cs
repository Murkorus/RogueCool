using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 250;
    public Rigidbody2D rb;
    Vector2 Movement;
    void Start()
    {
        rb.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = rb.transform.position - transform.position;
        dir = dir.normalized;
        
        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");
    }
    
     private void FixedUpdate() 
     {
         rb.velocity = new Vector2((Movement.x * speed) * Time.deltaTime,(Movement.y * speed) * Time.deltaTime);
     } 
    
}
