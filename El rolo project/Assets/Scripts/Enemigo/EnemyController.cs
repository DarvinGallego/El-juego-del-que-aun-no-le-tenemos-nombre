using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [Header("Variables de distancia al jugador")]
    public Transform PJ;
    [SerializeField] private float distancia;
    [Range(0, 2)][SerializeField] private float distanciaLimite;

    [Header("Variables de tiempo de patrullaje")]
    public int rutina;
    [SerializeField] private float cronometro;

    [Header("Variables de ataque")]
    [Range(0, 1)][SerializeField] private float rangoAtaque;
    public Collider2D hit;
    [SerializeField] private bool puedeAtacar;

    [Header("Variables de daño recibido")]
    public int vida;
    public bool recibioDaño;
    [SerializeField] private float empuje;

    [Header("Variables generales")]
    public float velocidad;
    public float velocidadLimite;
    [SerializeField] private int direccionGiro;
    public EstadoEnemigo estado = EstadoEnemigo.Patrullando;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(CronometroPatrullar());
    }

    // Update is called once per frame
    void Update()
    {
        distancia = Vector2.Distance(transform.position, PJ.position);
        Salud();

        if (!recibioDaño)
        {
            if (distancia < distanciaLimite && distancia > rangoAtaque)
            {
                estado = EstadoEnemigo.Persiguiendo;
            }
            else if (distancia > distanciaLimite)
            {
                estado = EstadoEnemigo.Patrullando;
            }
            else if (distancia < rangoAtaque)
            {
                estado = EstadoEnemigo.Atacando;
            }
        }
        else
        {
            estado = EstadoEnemigo.Herido;
        }

        switch (estado)
        {
            case EstadoEnemigo.Patrullando:
                Patrullar();
                break;
            case EstadoEnemigo.Persiguiendo:
                Perseguir();    
                break;
            case EstadoEnemigo.Atacando:
                Atacar();
                break;
            case EstadoEnemigo.Herido:
                Herido();
                break;
        }
    }

    public void Patrullar()
    { 
        switch (rutina)
        {
            case 0:
                animator.SetBool("walk", false);
                break;

            case 1:
                direccionGiro = Random.Range(0, 2);
                rutina++;
                break;

            case 2:
                switch (direccionGiro)
                {
                    case 0:
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        transform.Translate(Vector3.left * velocidad * Time.deltaTime);
                        break;

                    case 1:
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                        transform.Translate(Vector3.left * velocidad * Time.deltaTime);
                        break;
                }
                animator.SetBool("walk", true);
                break;
        }
    }

    public void Perseguir()
    {
        if (transform.position.x < PJ.position.x)
        {
            direccionGiro = 0;
        }
        else
        {
            direccionGiro = 1;
        }

        switch (direccionGiro)
        {
            case 0:
                transform.rotation = Quaternion.Euler(0, 180, 0);
                transform.position = Vector2.MoveTowards(transform.position, PJ.position, velocidadLimite * Time.deltaTime);
                break;

            case 1:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.position = Vector2.MoveTowards(transform.position, PJ.position, velocidadLimite * Time.deltaTime);
                break;
        }

        animator.SetBool("walk", true);     
    }

    public void Herido()
    {
        animator.SetBool("damaged", recibioDaño);
        animator.SetBool("walk", false);
        animator.SetBool("atack", false);

        if (transform.position.x < PJ.position.x)
        {
            direccionGiro = 0;
        }
        else
        {
            direccionGiro = 1;
        }

        switch (direccionGiro)
        {
            case 0:
                transform.rotation = Quaternion.Euler(0, 180, 0);
                transform.Translate(Vector3.left * empuje * Time.deltaTime, Space.World);
                break;

            case 1:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.Translate(Vector3.right * empuje * Time.deltaTime, Space.World);
                break;
        }
    }

    public void Salud()
    {
        if (vida <= 0) transform.gameObject.SetActive(false);
    }

    public void FinHerido()
    {
        recibioDaño = false;
        animator.SetBool("damaged", recibioDaño);
    }

    public void Atacar()
    {
        animator.SetBool("walk", false);
        animator.SetBool("atack", true);
    }
    
    public void PuñoInicio()
    {
        hit.enabled = true;
    }

    public void PuñoFin()
    {
        hit.enabled = false;
        animator.SetBool("walk", true);
        animator.SetBool("atack", false);
    }

    IEnumerator CronometroPatrullar()
    {
        while (true)
        {
            rutina = Random.Range(0, 2);
            yield return new WaitForSeconds(cronometro);
        }
    }
}