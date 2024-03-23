using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MostrarMunicion : MonoBehaviour
{
    public PlayerController player;
    private TextMeshProUGUI texto;

    void Start()
    {
        texto = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        texto.text = "Ammo " + player.municion.ToString() + "/" + player.municionMax.ToString();
    }
}