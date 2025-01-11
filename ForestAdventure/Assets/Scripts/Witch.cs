using System.Collections;
using UnityEngine;

public class Witch : MonoBehaviour
{
    private GameObject player; 
    private Transform playerTransform;
    public float hoverHeight = 5f; 
    public float moveSpeed = 5f;
    public float xThreshold = 3f; 
    public float yThreshold = 5f; 

    public GameObject fireballPrefab; 

    private Animator anim; 
    private float fireballCooldown = 2f;
    private float nextFireballTime = 0f; 
    private bool isFlipped = false; 

    void Start()
    {
        anim = GetComponent<Animator>(); 
        player = GameObject.FindGameObjectWithTag("Player"); 
        playerTransform = player.GetComponentInChildren<Transform>();
    }

    void Update()
    {
        MoveTowardsPlayer(); // Move towards player if she is out of range
        HandleFireballAttack(); // Control fireballs
    }

    void MoveTowardsPlayer()
    {
        // Calculate desired position
        Vector3 targetPosition = new Vector3(
            playerTransform.position.x,
            playerTransform.position.y + hoverHeight,
            transform.position.z
        );

        // Calculate difference in position
        float distanceX = Mathf.Abs(playerTransform.position.x - transform.position.x);
        float distanceY = Mathf.Abs(playerTransform.position.y + hoverHeight - transform.position.y);

        // Move to objective if it is out of limits
        if (distanceX > xThreshold || distanceY > yThreshold)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Only change direction if change is significant
            if ((playerTransform.position.x > transform.position.x && isFlipped) ||
                (playerTransform.position.x < transform.position.x && !isFlipped))
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        
        isFlipped = !isFlipped;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void HandleFireballAttack()
    {
        
        if (Time.time > nextFireballTime)
        {
            nextFireballTime = Time.time + fireballCooldown;
            LaunchFireball();
        }
    }

    void LaunchFireball()
    {
        
        anim.SetBool("Attack", true);

        
        GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        Rigidbody2D fireballRb = fireball.GetComponent<Rigidbody2D>();

        
        Vector2 direction = isFlipped ? Vector2.left : Vector2.right;

        
        int x = Random.Range(5, 8); // Fuerza horizontal
        int y = Random.Range(-1, 1); // Fuerza vertical mínima
        fireballRb.AddForce(new Vector2(fireballRb.mass * x * direction.x, fireballRb.mass * y), ForceMode2D.Impulse);

        
        Invoke("ResetAttackAnimation", 0.5f);
    }

    void ResetAttackAnimation()
    {
        anim.SetBool("Attack", false);
    }
}
