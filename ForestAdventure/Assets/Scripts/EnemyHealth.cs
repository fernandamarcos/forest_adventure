using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    private Rigidbody2D rb;
    private Collider2D collider;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        SetCurrentHealth(10);      
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    protected override void Die()
    {
        Enemy enemy = GetComponent<Enemy>();
        enemy.enabled = false;

        // Activar la gravedad para que caiga
        rb.gravityScale = 1;

        // Desactivar el collider para que no interactúe más con otros objetos
        collider.enabled = false;
        rb.transform.rotation = Quaternion.Euler(0f, 0f, -45f);
        rb.AddForce(new Vector2(0, rb.mass*5f), ForceMode2D.Impulse);  // Ajusta el valor según lo que necesites


        // Iniciar la corutina para destruir el enemigo cuando llegue a y = -9
        StartCoroutine(DestroyWhenFallen());
    }

    private IEnumerator DestroyWhenFallen()
    {

        float x = transform.position.x;
        // Esperar hasta que el enemigo llegue a y = -9
        while (transform.position.y > -9f)
        {
            // Bloquear la posición en X para que no se mueva
            transform.position = new Vector3(x, transform.position.y, transform.position.z);

            yield return null;  // Espera un frame
        }

        // Destruir el enemigo cuando haya caído lo suficiente
        Destroy(gameObject);
    }
}
