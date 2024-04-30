using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//sistema de guardado basado en archivos JSON

public class DataController : MonoBehaviour
{
    public GameObject PJ;
    public PlayerController PC;
    public string archivoGuardado;
    public DatosPJ datos = new DatosPJ();

    private void Awake()
    {
        archivoGuardado = Application.dataPath + "/datosJuego.json";

        PJ = GameObject.FindWithTag("Player");
        PC = PJ.GetComponent<PlayerController>();

        CargarDatos();
    }

    //si se cambian las funciones cargar y guardar por publicas, pueden ser llamadas en el un trigger
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CargarDatos();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            GuardarDatos();
        }
    }

    private void CargarDatos()
    {
        if (File.Exists(archivoGuardado))
        {
            string contenido = File.ReadAllText(archivoGuardado);
            datos = JsonUtility.FromJson<DatosPJ>(contenido);

            PJ.transform.position = datos.posicion;
            PC.vidaPJ = datos.vida;
            PC.municion = datos.municion;

            Debug.Log("Posicion jugador :" + datos.posicion);
        }
        else
        {
            Debug.Log("El archivo no existe");
        }
    }

    private void GuardarDatos()
    {
        DatosPJ nuevosDatos = new DatosPJ()
        {
            posicion = PJ.transform.position,
            vida = PC.vidaPJ,
            municion = PC.municion
        };

        string cadenaJSON = JsonUtility.ToJson(nuevosDatos);

        File.WriteAllText(archivoGuardado, cadenaJSON);

        Debug.Log("Archivo guardado");
    }
}