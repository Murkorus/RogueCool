using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    Vector3 Movement;
    public Animator anim;
    public Player_Hands hands;
 
    public float test1;
    public Vector3 test2;
    void Start()
    {
        rb.GetComponent<Rigidbody2D>();
        anim.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        test2 = mouseWorldPos;
        
    }

    void Move()
    {
        float horizontal= Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        if (horizontal == 0 && vertical == 0)
        {
            rb.velocity = new Vector2(0, 0);
            return;
        }

        rb.velocity = Movement * (speed);
        Movement = new Vector2(horizontal, vertical);
        
    }

    private void FixedUpdate()
    {
        Move();
        animate();
    }

    public void animate()
    {
        anim.SetFloat("MovementX",Movement.x);
        anim.SetFloat("MovementY",Movement.y);
    }
}
