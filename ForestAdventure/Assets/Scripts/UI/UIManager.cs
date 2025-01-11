using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    
    [SerializeField] private GameObject pauseMenu;
    // Sonido si quieres

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0) 
        {
            PauseGame(false);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && SceneManager.GetActiveScene().buildIndex != 0)
        {
            if(pauseMenu.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);

        }
    }

    public void MainMenu()
    {
        Debug.Log("Se ha invocado boton menu");
        SceneManager.LoadScene(0);
    }


    public void Quit()
    {
        //Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void PauseGame(bool status)
    {
        pauseMenu.SetActive(status);
        if (status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
