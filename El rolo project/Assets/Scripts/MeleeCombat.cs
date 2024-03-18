using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat: MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("El jugador recibio daño");
            PlayerController Player = collision.GetComponent<PlayerController>();

            Player.fueHerido = true;
            Player.vidaPJ--;

            Retroceso(Player);
        }
        else if (collision.CompareTag("Enemigos"))
        {
            Debug.Log("El enemigo recibio daño");
            EnemyController Enemy = collision.GetComponent<EnemyController>();

            Enemy.recibioDaño = true;
            Enemy.vida--;
        }
    }

    //Hace retroceder al jugador a una distancia especifica siempre mirando al enemigo
    void Retroceso(PlayerController player)
    {
        if (player.transform.position.x > transform.position.x)
        {
            player.transform.rotation = Quaternion.Euler(0, 180, 0);
            player.lookRigth = false;

            if (player.empujePJ.x < 0)
            {
                player.empujePJ.x *= 1;
            }
            else
            {
                player.empujePJ.x *= -1;
            }
        }
        else
        {
            player.transform.rotation = Quaternion.Euler(0, 0, 0);
            player.lookRigth = true;

            if (player.empujePJ.x < 0)
            {
                player.empujePJ.x *= -1;
            }
            else
            {
                player.empujePJ.x *= 1;
            }
        }
    }
}