using UnityEngine;
using UnityEngine.UIElements;

public class Crab : GroundEnemy
{
    private Animator anim;
    protected override void Start()
    {
        base.Start();  // Llamamos al Start de la clase base
        SetJumpForce(5f);  // Ajustamos la fuerza de salto específica para el cangrejo
    }

    protected override void HandleWallHit()
    {
        // Si el cangrejo golpea una pared, salta sobre ella
        StartJump();  // Hace que el cangrejo salte
    }

    protected override void ChangeDirection()
    {
        if (rb.position.x >= maxX-1)
        {
            transform.position = new Vector2(transform.position.x - 1f, transform.position.y); // Mueve un poco hacia atrás
        }
        else if (rb.position.x <= minX)
        {
            transform.position = new Vector2(transform.position.x + 1f, transform.position.y); // Mueve un poco hacia adelante
        }

        movingRight = !movingRight;

    }

    protected override void AttackPlayer(GameObject player)
    {
        Health playerHealth = player.GetComponent<Health>();
        
        if (playerHealth != null)
        {
            anim.SetTrigger("Attack");
            playerHealth.TakeDamage(attackDamage);
            Debug.Log("Jugador atacado con " + attackDamage + " de daño.");
        }

    }

}

