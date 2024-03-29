using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MostrarOpcionesEnJuego : MonoBehaviour
{
    [SerializeField] private GameObject MenuOP;

    private void Start()
    {
        MenuOP = MenuOpciones.Instance.panelMenu;
    }

    private void Update()
    {
        OcultarOpciones();
    }

    public void MostrarOpciones()
    {
        MenuOP.SetActive(true);
    }

    public void OcultarOpciones()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuOP.SetActive(false);
            MenuOP.GetComponentInParent<MenuInicial>().RegresoPantallaInicio();
        }
    }
}