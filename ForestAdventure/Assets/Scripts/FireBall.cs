using UnityEngine;

public class Fireball : Enemy
{
    [SerializeField] private float fireballSpeed = 10f;  
    [SerializeField] private float lifetime = 5f;  
    [SerializeField] private Rigidbody2D rb;  
    [SerializeField] private Collider2D col; 
    [SerializeField] private PhysicsMaterial2D bounceMaterial; 

    protected override void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        // Assign bounce material
        col.sharedMaterial = bounceMaterial;
        //Destroy ball after some time
        Destroy(gameObject, lifetime);
    }


    protected override void Update()
    {
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("Player")) // Check if object is player
        {
            Die();
        }
    }
}
