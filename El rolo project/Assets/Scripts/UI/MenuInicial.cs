using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    //Funciones especificas para el CanvasInicial:
    //permite entrar al juego y salir de este mismo
    public void Jugar()
    {
        SceneManager.LoadScene(1);
    }
    public void Salir()
    {
        Debug.Log("Saliste del juego");
        Application.Quit();
    }

    //Funcion especifica para el CanvasOpciones:
    //permite volver a la pantalla de inicio desde cualquier parte del juego
    public void PantallaInicial()
    {
        if (SceneManager.GetActiveScene().name != "StartMenuScene")
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            RegresoPantallaInicio();
            Debug.Log("Estas en la escena inicial");
        }
    }

    //Funcion especifica para el CanvasOpciones:
    //permite volver a la pantalla de inicio desde el menu de opciones en la escena inicial
    public void RegresoPantallaInicio()
    {
        GameObject canvasinicio, menuinicio;

        if (SceneManager.GetActiveScene().name == "StartMenuScene")
        {
            canvasinicio = GameObject.Find("CanvasInicial");
            menuinicio = canvasinicio.transform.Find("MenuPrincipal").gameObject;
            menuinicio.SetActive(true);
        }
    }
}