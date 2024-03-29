using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private bool juegoPausado = false;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (juegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausa();
            }
        }
    }

    public void Pausa()
    {
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
        juegoPausado = true;
        Time.timeScale = 0f;
    }
    
    public void Reanudar()
    {
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
        juegoPausado = false;
        Time.timeScale = 1f;
    }

    public void Reiniciar()
    {
        juegoPausado = false;
        Time.timeScale = 1f;
        //Recarga la escena en curso, aplica para todas las escenas de juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}