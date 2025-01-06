using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        SetCurrentHealth(50);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    protected override void Die()
    {

        Destroy(gameObject);
    }
}
