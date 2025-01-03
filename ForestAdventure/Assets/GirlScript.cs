using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlScript : MonoBehaviour
{
    public float JumpForce;
    public float Speed;
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(KeyCode.W))
        {
            Jump();
        }
        
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up*JumpForce);
    }
    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal, Rigidbody2D.velocity.y)*Speed;
    }
}
