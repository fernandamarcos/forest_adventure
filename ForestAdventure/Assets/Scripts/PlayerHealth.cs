using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    protected override void Start()
    {
        base.Start();
        SetCurrentHealth(100);
    }
    protected override void Die()
    {
        anim.SetTrigger("Death");
        GetComponent<WarriorMovement>().enabled = false;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);    
         
        if (currentHealth > 0)
        { // Animación de daño
            anim.SetTrigger("Hurt");
        }


    }


}
