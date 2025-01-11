using System.Collections;
using UnityEngine;

public class EnemyHealth : Health
{
    private Rigidbody2D rb;
    private Collider2D collider;
    private EnemySpawner enemySpawner;  
    protected override void Start()
    {
        base.Start();
        SetCurrentHealth(10); 
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    protected override void Die()
    {
        
        Enemy enemy = GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.enabled = false;
        }

        // Activate gravity for enemy to fall
        rb.gravityScale = 1;

        // Deactivate collider so it does not interact with other objects
        collider.enabled = false;
        rb.transform.rotation = Quaternion.Euler(0f, 0f, -45f);
        rb.AddForce(new Vector2(0, rb.mass * 5f), ForceMode2D.Impulse); 

        // Notify spawner
        if (enemySpawner != null)
        {
            enemySpawner.OnEnemyDeath();
        }

        // Start corroutine to destroy enemy once it is out of the scene
        StartCoroutine(DestroyWhenFallen());
    }

    // Corroutine
    private IEnumerator DestroyWhenFallen()
    {
        float x = transform.position.x;

        // Wait until enemy is at y=-9 to destroy it
        while (transform.position.y > -9f)
        {
            transform.position = new Vector3(x, transform.position.y, transform.position.z); 
            yield return null;
        }

        
        Destroy(gameObject);
    }

    public void SetEnemySpawner(EnemySpawner spawner)
    {
        enemySpawner = spawner;
    }
}
