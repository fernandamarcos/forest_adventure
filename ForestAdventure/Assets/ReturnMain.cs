using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{
    // El tiempo en segundos que esperar� antes de volver al men� principal
    public float delayTime = 7f;

    void Start()
    {
        // Llama al m�todo para volver al men� despu�s de 4 segundos
        Invoke("LoadMainMenu", delayTime);
    }

    void LoadMainMenu()
    {
        // Cargar la escena del men� principal (aseg�rate de que la escena est� en la Build Settings)
        SceneManager.LoadScene("MainMenu");  // Cambia "MainMenu" por el nombre de tu escena de men� principal
    }
}
