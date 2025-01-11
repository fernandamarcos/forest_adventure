using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bat : Enemy
{
    public float speed = 3f; 
    public float minX = -6f; 
    public float maxX = 45f;  
    public float idleThreshold = 0.1f; 

    private Vector2 startPosition;
    private bool movingRight = true; 
    private Animator animator;
    private Rigidbody2D rb;

    protected override void Start()
    {
        
        startPosition = transform.position;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    protected override void Update()
    {
        
        MoveBat();
    }

    void MoveBat()
    {
        
        float targetX = movingRight ? maxX : minX;

        
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetX, transform.position.y), speed * Time.deltaTime);

        
        animator.SetBool("flying", true);

        
        if (Mathf.Abs(transform.position.x - targetX) < idleThreshold)
        {
            movingRight = !movingRight;
            RotateBat();
        }
    }

    
    void RotateBat()
    {
        if (movingRight)
        {
            transform.localScale = new Vector3(5, 4, 1); // Rotación a la derecha
        }
        else
        {
            transform.localScale = new Vector3(-5, 4, 1); // Rotación a la izquierda
        }
    }

}
