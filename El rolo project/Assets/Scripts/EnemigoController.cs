using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemigoController : MonoBehaviour
{
    private Animator animator;
    public GameObject target;

    public int rutina;
    public int direccion;
    public float cronometro;
    public float cronoregulador;
    public float vel_walk;
    public float vel_run;
    public bool atacando;

    public float rango_vision;
    public float rango_ataque;
    public GameObject rango;
    public GameObject hit;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Comportamientos();
    }

    public void Comportamientos()
    {
        if (Mathf.Abs(transform.position.x - target.transform.position.x) > rango_vision && !atacando)
        {
            cronometro += 1 * Time.deltaTime;

            if (cronometro >= cronoregulador)
            {
                rutina = UnityEngine.Random.Range(0, 2);
                cronometro = 0;
            }

            switch (rutina)
            {
                case 0:
                    animator.SetBool("walk", false);
                    break;

                case 1:
                    direccion = UnityEngine.Random.Range(0, 2);
                    rutina++;
                    break;

                case 2:
                    switch (direccion)
                    {
                        case 0:
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                            transform.Translate(Vector3.left * vel_walk * Time.deltaTime);
                            break;

                        case 1:
                            transform.rotation = Quaternion.Euler(0, 180, 0);
                            transform.Translate(Vector3.left * vel_walk * Time.deltaTime);
                            break;
                    }
                    animator.SetBool("walk", true);
                    break;
            }
        }
        else
        {
            if (Mathf.Abs(transform.position.x - target.transform.position.x) > rango_ataque && !atacando)
            {
                if(transform.position.x < target.transform.position.x)
                {
                    transform.Translate(Vector3.right * vel_run * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    animator.SetBool("atack", false);
                }
                else
                {
                    transform.Translate(Vector3.right * vel_run * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    animator.SetBool("atack", false);
                }
            }
            else
            {
                if (!atacando)
                {
                    if(transform.position.x < target.transform.position.x)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                    animator.SetBool("walk", false);
                }
            }
        }
    }

    public void Final_animacion()
    {
        animator.SetBool("atack", false);
        atacando = false;
        rango.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void ColliderArmaActiva()
    {
        hit.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void ColliderArmaInactiva()
    {
        hit.GetComponent<BoxCollider2D>().enabled = false;
    }
}