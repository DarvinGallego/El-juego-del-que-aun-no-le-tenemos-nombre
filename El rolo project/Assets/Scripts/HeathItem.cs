using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController Player = collision.GetComponent<PlayerController>();

            if (Player.vidaPJ < Player.vidaPJMax)
            {
                Player.vidaPJ++;
                Destroy(gameObject);
            }
        }
    }
}