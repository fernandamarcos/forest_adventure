using UnityEngine;

public abstract class GroundEnemy : Enemy
{
    public float speed = 10f;  // Velocidad de movimiento
    public float minX = -6f;  // Límite inferior (inicio en X)
    public float maxX = 20f;  // Límite superior (fin en X)
    protected bool movingRight = true;  // Dirección de movimiento

    protected Rigidbody2D rb;
    protected bool isJumping = false;   // Estado de salto

    protected float wallCheckDistance = 0.5f;  // Distancia para verificar si hay una pared
    protected float jumpForce = 6f; // Fuerza del salto
    protected float forwardJumpForce = 1f; // Impulso hacia adelante durante el salto

    // Start se llama cuando el objeto se inicializa
    protected override void Start()
    {
        base.Start();  // Llamamos al Start de la clase base
        rb = GetComponent<Rigidbody2D>();
        attackDamage = 10; // Daño personalizado de los enemigos de tierra

        if (rb != null)
        {
            // Bloquea la rotación en el eje Z para evitar que de volteretas
            rb.freezeRotation = true;
        }
    }

    // Update se llama una vez por frame
    protected override void Update()
    {
        base.Update();  // Llamamos al Update de la clase base

        if (!isJumping)
        {
            Move();  // Si no está saltando, mueve al enemigo
        }
        else
        {
            CheckIfJumpFinished();  // Verifica si el salto ha terminado
        }
    }

    // Movimiento general del enemigo
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
        // Este método será sobrescrito en las clases hijas para definir el comportamiento específico al golpear una pared.
    }

    // Método para hacer que el enemigo comience a saltar
    protected virtual void StartJump()
    {
        if (!isJumping)  // Evita saltos consecutivos
        {
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, 0f);  // Resetear la velocidad Y antes de aplicar el salto
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);  // Aplicar fuerza hacia arriba para simular el salto

            // Impulso hacia adelante
            float moveDirection = movingRight ? 1f : -1f; // Determinar si va hacia la derecha o izquierda
            rb.velocity = new Vector2(moveDirection * forwardJumpForce + rb.velocity.x, rb.velocity.y);  // Aplica el impulso hacia adelante
        }
    }

    // Método para verificar si el salto ha terminado
    private void CheckIfJumpFinished()
    {
        if (rb.velocity.y <= 0f)  // Cuando la velocidad Y sea 0 o negativa, significa que está cayendo
        {
            isJumping = false;  // El salto ha terminado
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Time.time >= nextAttackTime)
        {
            // Atacar al jugador
            AttackPlayer(collision.gameObject);
            // Reiniciar cooldown de ataque
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    protected virtual void AttackPlayer(GameObject player)
    {
        // Sobre-escrito en clases hijas
    }
}
