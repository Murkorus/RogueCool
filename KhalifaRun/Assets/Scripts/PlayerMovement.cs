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
    public Vector3 movedirection;
    public Vector3 mouseposdirection;

    public Player_Hands hands;

    [Header("Dash Settings")] 
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration;
    [SerializeField] float dashCooldown;
    [SerializeField] bool isDashing;
    [SerializeField] bool canDash;

    
    
    void Start()
    {
        canDash = true;
        rb.GetComponent<Rigidbody2D>();
        anim.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Move()
    {
        float horizontal= Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        if (horizontal == 0 && vertical == 0)
        {
            rb.velocity = new Vector2(0, 0);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(dash());
        }
        rb.velocity = Movement * (speed * Time.deltaTime);
        Movement = new Vector2(horizontal, vertical);

        movedirection = new Vector2(horizontal, vertical).normalized;
        mouseposdirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        Move();
        animate();
    }

    public void animate()
    {
        anim.SetFloat("MovementX",Movement.x);
        anim.SetFloat("MovementY",Movement.y);
    }

    private IEnumerator dash()
    {
        isDashing = true;
        
        rb.velocity = new Vector2(movedirection.x * dashSpeed, movedirection.y * dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
