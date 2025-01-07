using System.Collections;
using UnityEngine;

public class EnemyHealth : Health
{
    private Rigidbody2D rb;
    private Collider2D collider;
    private EnemySpawner enemySpawner;  // Referencia al EnemySpawner

    protected override void Start()
    {
        base.Start();
        SetCurrentHealth(10); // Configurar la salud inicial
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    protected override void Die()
    {
        // Desactivar el comportamiento del enemigo
        Enemy enemy = GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.enabled = false;
        }

        // Activar la gravedad para que caiga
        rb.gravityScale = 1;

        // Desactivar el collider para que no interactúe más con otros objetos
        collider.enabled = false;
        rb.transform.rotation = Quaternion.Euler(0f, 0f, -45f);
        rb.AddForce(new Vector2(0, rb.mass * 5f), ForceMode2D.Impulse); // Ajustar el empuje

        // Notificar al spawner que el enemigo ha muerto
        if (enemySpawner != null)
        {
            enemySpawner.OnEnemyDeath();
        }

        // Iniciar la corutina para destruir el enemigo al caer fuera del nivel
        StartCoroutine(DestroyWhenFallen());
    }

    private IEnumerator DestroyWhenFallen()
    {
        float x = transform.position.x;

        // Esperar hasta que el enemigo llegue a y = -9
        while (transform.position.y > -9f)
        {
            transform.position = new Vector3(x, transform.position.y, transform.position.z); // Bloquear posición X
            yield return null;
        }

        // Destruir el enemigo cuando haya caído lo suficiente
        Destroy(gameObject);
    }

    public void SetEnemySpawner(EnemySpawner spawner)
    {
        enemySpawner = spawner;
    }
}
