using UnityEngine;

public class Rat : GroundEnemy
{
    protected override void Start()
    {
        base.Start();  
        SetJumpForce(4f);  
        SetSpeed(1);
    }

    protected override void HandleWallHit()
    {
        // Si la rata golpea una pared, cambia de dirección y no salta
        ChangeDirection();  // Simplemente cambia de dirección sin saltar
    }
}
