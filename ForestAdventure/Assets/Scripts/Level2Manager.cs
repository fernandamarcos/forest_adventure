using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Manager : MonoBehaviour
{
    [SerializeField] public  AudioClip levelMusic;          
    [SerializeField] public AudioSource musicPlayer;       


    private PlayerHealth playerHealth;    

    void SetupMusic()
    {
        if (musicPlayer != null && levelMusic != null)
        {
            musicPlayer.clip = levelMusic;
            musicPlayer.loop = true; 
            musicPlayer.Play();
        }
        
    }

}
