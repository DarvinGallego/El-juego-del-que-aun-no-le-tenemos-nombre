using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene(1);
    }

    public void Salir()
    {
        Debug.Log("Saliste del juego");
        Application.Quit();
    }
}