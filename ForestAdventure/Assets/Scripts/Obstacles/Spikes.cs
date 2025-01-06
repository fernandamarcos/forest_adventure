using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private int damage;

     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("SPIKES CONTRA PLAYER");
            collision.GetComponent<Health>().TakeDamage(damage);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
