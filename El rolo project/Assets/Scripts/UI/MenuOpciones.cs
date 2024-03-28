using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MenuOpciones : MonoBehaviour
{
    [SerializeField] private AudioMixer BGM;
    [SerializeField] private Slider BGMSlider;

    [SerializeField] private AudioMixer SFX;
    [SerializeField] private Slider SFXSlider;

    [SerializeField] private Image brillo;
    [SerializeField] private Slider brilloSlider;

    [SerializeField] private Resolution[] resoluciones;
    [SerializeField] private TMP_Dropdown resolucionDropdown;

    [SerializeField] private TMP_Dropdown calidadDropdown;

    [SerializeField] private Toggle pantallaToggle;

    private void Start()
    {
        ConfirmarPantallaCompleta();
        ResolucionGrafica();
        CalidadGrafica();
        CargarVolumenBGM();
        CargarVolumenSFX();
        CargarBrillo();
    }

    public void ActivarPantallaCompleta(bool PC)
    {
        Screen.fullScreen = PC;
    }

    public void CambiarCalidad()
    {
        //Guarda la calidad guardada 
        PlayerPrefs.SetInt("ValorCalidad", calidadDropdown.value);
        QualitySettings.SetQualityLevel(calidadDropdown.value);
    }

    public void VolumenSFX()
    {
        //Guarda el volumen asignado de los efectos de sonido
        PlayerPrefs.SetFloat("VolSFX", SFXSlider.value);
        SFX.SetFloat("SFXVOL", SFXSlider.value);
    }
    public void VolumenBGM()
    {
        //Guarda el volumen asignado de la ambientacion
        PlayerPrefs.SetFloat("VolBGM", BGMSlider.value);
        BGM.SetFloat("BGMVOL", BGMSlider.value);
    }

    public void Brillo()
    {
        //Guarda el valor alpha del slider cambiando la imagen dependiendo la condicion
        PlayerPrefs.SetFloat("AlfaB", brilloSlider.value);
        float B = brilloSlider.value;

        if (B <= 0)
        {
            brillo.color = new Color(0, 0, 0, -B);  //Negro - brillo bajo
        }
        else
        {
            brillo.color = new Color(255, 255, 255, B);  //Blanco - brillo alto
        }
    }

    public void CambiarResolucion(int indice)
    {
        //Guarda la resolucion, iniciara con la ultima resolucion guardada
        PlayerPrefs.SetInt("ValorResolucion", resolucionDropdown.value);

        Resolution resolucion = resoluciones[indice];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
    }

    //Apartir de aqui se separan las funciones de cargado con las que asignan
    //los valores a las distintas configuraciones

    public void ResolucionGrafica()
    {
        //Define todas las resoluciones del dispositivo y genera una lista apartir de estas
        resoluciones = Screen.resolutions;
        resolucionDropdown.ClearOptions();
        List<String> opciones = new List<String>();
        int resolucionActual = 0;

        for (int i = 0; i < resoluciones.Length;  i++)
        {
            string opcion = resoluciones[i].width + "x" + resoluciones[i].height;
            opciones.Add(opcion);

            if (Screen.fullScreen && resoluciones[i].width == Screen.currentResolution.width && resoluciones[i].height == Screen.currentResolution.height)
            {
                resolucionActual = i;
            }
        }
        //Las opciones generadas se muestran en el dropdown de manera personalizada a cada dispositivo
        resolucionDropdown.AddOptions(opciones);
        resolucionDropdown.value = resolucionActual;
        resolucionDropdown.RefreshShownValue();

        //Carga la resolucion, iniciara por defecto con la resolucion asignada
        resolucionDropdown.value = PlayerPrefs.GetInt("ValorResolucion", resolucionActual);
    }

    public void CalidadGrafica()
    {
        //Carga la calidad
        calidadDropdown.value = PlayerPrefs.GetInt("ValorCalidad");
        CambiarCalidad();
    }

    public void ConfirmarPantallaCompleta()
    {
        //Confima el estado inicial de la pantalla
        if (Screen.fullScreen)
        {
            pantallaToggle.isOn = true;
        }
        else
        {
            pantallaToggle.isOn = false;
        }
    }

    public void CargarVolumenSFX()
    {
        //Carga el volumen de los efectos de sonido
        SFXSlider.value = PlayerPrefs.GetFloat("VolSFX");
        VolumenSFX();
    }
    public void CargarVolumenBGM()
    {
        //Carga el volumen de la ambientacion
        BGMSlider.value = PlayerPrefs.GetFloat("VolBGM");
        VolumenBGM();
    }

    public void CargarBrillo()
    {
        //Carga el valor del alpha de la imagen que da el brillo
        brilloSlider.value = PlayerPrefs.GetFloat("AlfaB");
        Brillo();
    }
}