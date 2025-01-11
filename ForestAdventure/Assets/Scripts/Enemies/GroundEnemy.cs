using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public abstract class GroundEnemy : Enemy
{
    public float speed = 10f;  
    public float minX = -6f;  
    public float maxX = 20f;  
    protected bool movingRight = true;  

    protected Rigidbody2D rb;
    protected bool isJumping = false;   

    protected float wallCheckDistance = 0.5f;  
    protected float jumpForce = 6f; 
    protected float forwardJumpForce = 1f; 

    
    protected override void Start()
    {
        base.Start();  
        rb = GetComponent<Rigidbody2D>();
        attackDamage = 10; 

        if (rb != null)
        {
            
            rb.freezeRotation = true;
        }
    }

    
    protected override void Update()
    {
        base.Update();  

        if (!isJumping)
        {
            Move();  
        }
        else
        {
            CheckIfJumpFinished();  
        }
    }

 
    protected virtual void Move()
    {
        // Raycast para detectar paredes antes de moverse
        Vector2 direction = movingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, wallCheckDistance, LayerMask.GetMask("Wall"));

        // Si no hay pared, mueve al enemigo
        if (hit.collider == null)
        {
            // Movimiento continuo entre minX y maxX
            float targetX = movingRight ? maxX : minX;
            transform.position = new Vector2(Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime), transform.position.y);
        }
        else
        {
            // Cuando el enemigo se encuentra con una pared, invoca el comportamiento personalizado
            HandleWallHit();
        }

        // Cambio de dirección cuando alcanza los límites
        if (rb.position.x >= maxX || rb.position.x <= minX)
        {
            ChangeDirection();
        }
    }

    // Método para cambiar de dirección, se puede sobrescribir en las clases hijas
    protected virtual void ChangeDirection()
    {
        movingRight = !movingRight;  // Cambiar la dirección del movimiento

        Vector3 newScale = transform.localScale;
        newScale.x = -newScale.x;  // Invertir solo el eje X
        transform.localScale = newScale;
    }

    // Método para manejar lo que sucede cuando el enemigo golpea una pared
    protected virtual void HandleWallHit()
    {
    }

    
    protected virtual void StartJump()
    {
        if (!isJumping)  // Evita saltos consecutivos
        {
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, 0f);  
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);  

            // Impulso hacia adelante
            float moveDirection = movingRight ? 1f : -1f; 
            rb.velocity = new Vector2(moveDirection * forwardJumpForce + rb.velocity.x, rb.velocity.y);  
        }
    }

    
    private void CheckIfJumpFinished()
    {
        if (rb.velocity.y <= 0f)  
        {
            isJumping = false;  
        }
    }

    // Método para personalizar el jumpForce para cada enemigo (se sobrescribe en las clases hijas)
    protected virtual void SetJumpForce(float newJumpForce)
    {
        jumpForce = newJumpForce;
    }

    protected virtual void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

}
