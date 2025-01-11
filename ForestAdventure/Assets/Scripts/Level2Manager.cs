using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Manager : MonoBehaviour
{
    [SerializeField] public  AudioClip levelMusic;          // M�sica espec�fica para este nivel
    [SerializeField] public AudioSource musicPlayer;       // Componente AudioSource que reproducir� la m�sica


    private PlayerHealth playerHealth;    // Referencia al sistema de salud del jugador

    void SetupMusic()
    {
        if (musicPlayer != null && levelMusic != null)
        {
            musicPlayer.clip = levelMusic;
            musicPlayer.loop = true; // Reproducir en bucle
            musicPlayer.Play();
        }
        
    }

}