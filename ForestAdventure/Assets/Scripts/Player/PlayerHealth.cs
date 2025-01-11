using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    [SerializeField] private Slider healthSlider;  
    [SerializeField] private Image healthFillImage;  
    public event Action OnDeath;
    protected override void Start()
    {
        base.Start();
        SetCurrentHealth(100);  
        
        if (healthSlider != null)
        {
            healthSlider.maxValue = 100;  
            healthSlider.value = currentHealth;  
        }

        if (healthFillImage == null && healthSlider.fillRect != null)
        {
            healthFillImage = healthSlider.fillRect.GetComponent<Image>();  
        }
    }

    protected override void Die()
    {
        anim.SetTrigger("Death");
        GetComponent<WarriorMovement>().enabled = false;
        StartCoroutine(HandleDeath());

    }

    private IEnumerator HandleDeath()
    {
        
        yield return new WaitForSeconds(2f);

        
        OnDeath?.Invoke();
    }


    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);  

        
        if (currentHealth > 0)
        {
            anim.SetTrigger("Hurt");
        }

        
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;  

            
            UpdateHealthBarColor();
        }
    }

    
    private void UpdateHealthBarColor()
    {
        if (healthFillImage != null)
        {
            
            float healthPercentage = (float)currentHealth / (float)maxHealth;

            
            healthFillImage.color = Color.Lerp(Color.red, Color.green, healthPercentage);
        }
    }

    private void RestartHealthBar()
    {
        healthSlider.value = currentHealth;
        UpdateHealthBarColor();
    }

    public void Respawn()
    {
        // Restart player configuration

        ResetHealth();
        RestartHealthBar();
        anim.ResetTrigger("Death");
        anim.Play("Idle");
        GetComponent<WarriorMovement>().enabled = true;
    }

}
