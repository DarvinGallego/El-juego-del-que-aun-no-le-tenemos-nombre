using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public Transform PJ;
    [SerializeField] private float distancia;
    [Range(0, 2)][SerializeField] private float distanciaLimite;        
    [SerializeField] private float cronometro;
    [SerializeField] private float cronoregulador;
    [Range(0, 1)][SerializeField] private float rangoAtaque;
    [SerializeField] private bool puedeAtacar;
    [SerializeField] private int direccionGiro;
    public Collider2D hit;
    public int rutina;
    public float velocidad;
    public float velocidadLimite;
    public EstadoEnemigo estado = EstadoEnemigo.Patrullando;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        distancia = Vector2.Distance(transform.position, PJ.position);
        animator.SetFloat("Distancia", distancia);

        if (distancia < distanciaLimite && distancia > rangoAtaque)
        {
            estado = EstadoEnemigo.Persiguiendo;
        }
        else if (distancia > distanciaLimite)
        {
            estado = EstadoEnemigo.Patrullando;
        }
        else if(distancia < rangoAtaque)
        {
            estado = EstadoEnemigo.Atacando;
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
            case EstadoEnemigo.Buscando:
                //Buscar();
                break;
        }
    }

    public void Patrullar()
    {
        cronometro += 1 * Time.deltaTime;

        if (cronometro >= cronoregulador)
        {
            rutina = Random.Range(0, 2);
            cronometro = 0;
        }

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
}