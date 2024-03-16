using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Daño");
            PlayerController Player = collision.GetComponent<PlayerController>();

            Player.fueHerido = true;
            Player.vidaPJ--;

            if (Player.transform.position.x > transform.position.x)
            {
                Player.transform.rotation = Quaternion.Euler(0, 180, 0);
                Player.lookRigth = false;

                if (Player.empujePJ.x < 0)
                {
                    Player.empujePJ.x *= 1;
                }
                else
                {
                    Player.empujePJ.x *= -1;
                }
            }
            else
            {
                Player.transform.rotation = Quaternion.Euler(0, 0, 0);
                Player.lookRigth = true;

                if (Player.empujePJ.x < 0)
                {
                    Player.empujePJ.x *= -1;
                }
                else
                {
                    Player.empujePJ.x *= 1;
                }
            }
        }
    }
}