using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{

    protected override void Die()
    {
        anim.SetTrigger("Death");
        Debug.Log("Death triggered");
        GetComponent<WarriorMovement>().enabled = false;
    }

}
