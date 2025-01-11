using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorMovement : MonoBehaviour
{
    public float JumpForce;
    public float Speed;
    public float FallMultiplier = 2.5f; 
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    private bool grounded;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-13f, -3.16f, 0f);
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        transform.localScale = new Vector3(4, 4, 4);
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        
        Debug.DrawRay(transform.position, Vector3.down * 0.8f, Color.red);

        
        if (Horizontal > 0)
        {
            transform.localScale = new Vector3(4, 4, 4); // Escala normal
        }
        else if (Horizontal < 0)
        {
            transform.localScale = new Vector3(-4, 4, 4); // Invierte el eje X
        }

        
        animator.SetBool("running", Horizontal != 0.0f);

        
        grounded = Physics2D.Raycast(transform.position, Vector3.down, 0.8f);

        
        animator.SetBool("jumping", !grounded);

        
        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, 0); 
        Rigidbody2D.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);

        // Increase gravity if player is falling
        if (Rigidbody2D.velocity.y < 0)
        {
            Rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (FallMultiplier - 1) * Time.fixedDeltaTime;
        }
    }
}
