using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public PlayerController player;
    private Image barraVida;

    private void Start()
    {
        barraVida = GetComponent<Image>();
    }

    void Update()
    {
        barraVida.fillAmount = player.vidaPJ/player.vidaPJMax;
    }
}