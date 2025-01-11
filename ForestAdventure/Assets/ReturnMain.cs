using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{
    // El tiempo en segundos que esperará antes de volver al menú principal
    public float delayTime = 7f;

    void Start()
    {
        // Llama al método para volver al menú después de 4 segundos
        Invoke("LoadMainMenu", delayTime);
    }

    void LoadMainMenu()
    {
        // Cargar la escena del menú principal (asegúrate de que la escena esté en la Build Settings)
        SceneManager.LoadScene("MainMenu");  // Cambia "MainMenu" por el nombre de tu escena de menú principal
    }
}
