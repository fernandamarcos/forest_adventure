using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    [SerializeField] private Slider healthSlider;  // Referencia al Slider de salud
    [SerializeField] private Image healthFillImage;  // Referencia al Image del "Fill" del Slider (el color de la barra)

    protected override void Start()
    {
        base.Start();
        SetCurrentHealth(100);  // Establecer la salud inicial del jugador

        // Aseg�rate de que el Slider y el FillImage est�n asignados
        if (healthSlider != null)
        {
            healthSlider.maxValue = 100;  // M�ximo de salud (aj�stalo seg�n lo necesites)
            healthSlider.value = currentHealth;  // Establecer el valor actual de la salud
        }

        if (healthFillImage == null && healthSlider.fillRect != null)
        {
            healthFillImage = healthSlider.fillRect.GetComponent<Image>();  // Obtener el Image del Fill si no lo asignaste
        }
    }

    protected override void Die()
    {
        anim.SetTrigger("Death");
        GetComponent<WarriorMovement>().enabled = false;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);  // Restar vida

        // Si a�n tiene salud, muestra animaci�n de da�o
        if (currentHealth > 0)
        {
            anim.SetTrigger("Hurt");
        }

        // Actualiza el valor del Slider seg�n la nueva salud
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;  // Actualizar el valor del Slider

            // Cambiar el color de la barra de salud
            UpdateHealthBarColor();
        }
    }

    // M�todo para cambiar el color de la barra de salud
    private void UpdateHealthBarColor()
    {
        if (healthFillImage != null)
        {
            // Calcula el porcentaje de salud restante
            float healthPercentage = (float)currentHealth / (float)maxHealth;

            // Interpolamos entre rojo y verde basado en el porcentaje de salud
            healthFillImage.color = Color.Lerp(Color.red, Color.green, healthPercentage);
        }
    }
}
