using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject gameOverMenu;
    // Sonido si quieres

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        // Play sound si se quiere alguno
    }

}
