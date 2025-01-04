using UnityEngine;

public class Rat : GroundEnemy
{
    protected override void Start()
    {
        base.Start();  // Llamamos al Start de la clase base
        SetJumpForce(4f);  // Ajustamos la fuerza de salto específica para la rata
        SetSpeed(1);
        SetHealth(5);
    }

    protected override void HandleWallHit()
    {
        // Si la rata golpea una pared, cambia de dirección y no salta
        ChangeDirection();  // Simplemente cambia de dirección sin saltar
    }
}
