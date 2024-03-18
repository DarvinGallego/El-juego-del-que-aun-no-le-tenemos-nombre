using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController Player = collision.GetComponent<PlayerController>();

            if (Player.municion < Player.municionMax || !Player.tieneMunicion)
            {
                Player.municion += 5;
                Player.tieneMunicion = true;
                Destroy(gameObject);
            }
        }
    }
}